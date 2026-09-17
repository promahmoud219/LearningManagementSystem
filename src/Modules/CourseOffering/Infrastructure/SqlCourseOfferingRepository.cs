using LearningManagementSystem.Modules.CourseOffering.Application.Queries.GetAllCourseOfferings;
using LearningManagementSystem.Modules.CourseOffering.Application.Repositories;
using LearningManagementSystem.Modules.CourseOffering.Domain.Aggregates;
using LearningManagementSystem.SharedKernel.ValueObjects;
using Microsoft.Data.SqlClient;
using System.Data;
using CourseOfferingAggregate = LearningManagementSystem.Modules.CourseOffering.Domain.Aggregates.CourseOffering;

namespace LearningManagementSystem.Modules.CourseOffering.Infrastructure;

internal sealed class SqlCourseOfferingRepository(string connectionString) : ICourseOfferingRepository
{
    public async Task<CourseOfferingAggregate?> GetByIdAsync(
        CourseOfferingId id,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT co.CourseOfferingId, co.CourseId, co.TermId, co.StudyYear,
                   c.DepartmentId, co.Price, co.Capacity,
                   COUNT(e.EnrollmentId) AS CurrentEnrollmentCount
            FROM dbo.CourseOffering AS co
            INNER JOIN dbo.Course AS c ON c.CourseId = co.CourseId
            LEFT JOIN dbo.Enrollment AS e
                ON e.CourseOfferingId = co.CourseOfferingId
                AND e.StatusId IN (0, 1)
            WHERE co.CourseOfferingId = @Id
            GROUP BY co.CourseOfferingId, co.CourseId, co.TermId, co.StudyYear,
                     c.DepartmentId, co.Price, co.Capacity;
            """;

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id.Value;

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
            return null;

        return CourseOfferingAggregate.Rehydrate(
            new CourseOfferingId(reader.GetInt32(0)),
            new CourseId(reader.GetInt16(1)),
            new TermId(reader.GetByte(2)),
            reader.GetInt16(3),
            (Department)reader.GetByte(4),
            new Money(reader.GetDecimal(5), "EGP"),
            reader.GetInt16(6),
            reader.GetInt32(7));
    }

    public async Task AddAsync(CourseOfferingAggregate courseOffering, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO dbo.CourseOffering
                (CourseOfferingId, CourseId, StudyYear, Price, Capacity, TermId)
            VALUES
                (@Id, @CourseId, @StudyYear, @Price, @Capacity, @TermId);
            """;

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = courseOffering.Id.Value;
        command.Parameters.Add("@CourseId", SqlDbType.SmallInt).Value = courseOffering.CourseId.Value;
        command.Parameters.Add("@StudyYear", SqlDbType.SmallInt).Value = courseOffering.StudyYear;
        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = courseOffering.Price.Amount;
        command.Parameters.Add("@Capacity", SqlDbType.SmallInt).Value = courseOffering.Capacity;
        command.Parameters.Add("@TermId", SqlDbType.TinyInt).Value = courseOffering.TermId.Value;

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<Money> GetCurrentPriceAsync(CourseOfferingId courseOfferingId, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT Price
            FROM dbo.CourseOffering
            WHERE CourseOfferingId = @Id;
            """;
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = courseOfferingId.Value;
        var result = await command.ExecuteScalarAsync(cancellationToken);
        
        if (result is null || result == DBNull.Value)
            throw new InvalidOperationException(
                $"Course offering {courseOfferingId.Value} was not found.");

        return new Money((decimal)result, "EGP");
    }

    public async Task<IReadOnlyList<CourseOfferingListItem>> GetAllAsync(
    CancellationToken cancellationToken)
    {
        const string sql = """
        SELECT *
        FROM dbo.vw_CourseOfferingDetails
        ORDER BY CourseOfferingId;
        """;

        var courseOfferings = new List<CourseOfferingListItem>();

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        await using var reader =
            await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            courseOfferings.Add(
                new CourseOfferingListItem(
                    reader.GetInt32(0),        
                    reader.GetInt16(1),        
                    reader.GetString(2),       
                    reader.GetByte(3),         
                    reader.GetString(4),       
                    reader.GetByte(5),         
                    reader.GetInt16(6),        
                    reader.GetDecimal(7),      
                    reader.GetInt16(8),        
                    reader.GetInt32(9)));      
        }

        return courseOfferings;
    }
}
