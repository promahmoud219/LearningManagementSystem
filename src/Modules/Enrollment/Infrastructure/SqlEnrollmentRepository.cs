using System.Data;
using LearningManagementSystem.Modules.Enrollment.Application.Repositories;
using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;
using Microsoft.Data.SqlClient;
using EnrollmentAggregate = LearningManagementSystem.Modules.Enrollment.Domain.Aggregates.Enrollment;

namespace LearningManagementSystem.Modules.Enrollment.Infrastructure;

internal sealed class SqlEnrollmentRepository(string connectionString) : IEnrollmentRepository
{
    public async Task<EnrollmentId> AddAsync(
        EnrollmentAggregate enrollment,
        CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO dbo.Enrollment
                (StudentId, CourseOfferingId, StatusId, EnrollmentDate,
                 Price, DiscountTypeId, DiscountValue, DiscountCode)
            OUTPUT INSERTED.EnrollmentId
            VALUES
                (@StudentId, @CourseOfferingId, @StatusId, @EnrollmentDate,
                 @Price, NULL, @DiscountValue, @DiscountCode);
            """;

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@StudentId", SqlDbType.Int).Value = enrollment.StudentId.Value;
        command.Parameters.Add("@CourseOfferingId", SqlDbType.Int).Value = enrollment.CourseOfferingId.Value;
        command.Parameters.Add("@StatusId", SqlDbType.TinyInt).Value = (byte)enrollment.Status;
        command.Parameters.Add("@EnrollmentDate", SqlDbType.DateTime2).Value = enrollment.EnrollmentDate;
        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = enrollment.Price.Amount;
        command.Parameters.Add("@DiscountValue", SqlDbType.Decimal).Value = enrollment.Discount.Amount;
        command.Parameters.Add("@DiscountCode", SqlDbType.VarChar, 50).Value =
            (object?)enrollment.DiscountCode ?? DBNull.Value;

        var generatedId = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
        var enrollmentId = new EnrollmentId(generatedId);
        enrollment.AssignId(enrollmentId);

        return enrollmentId;
    }
}
