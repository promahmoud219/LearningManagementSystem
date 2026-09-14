using System.Data;
using LearningManagementSystem.Modules.Student.Domain.Aggregates;
using LearningManagementSystem.Modules.Student.Domain.Enums;
using LearningManagementSystem.Modules.Student.Application.Repositories;
using LearningManagementSystem.Modules.Student.Domain.ValueObjects;
using LearningManagementSystem.SharedKernel.ValueObjects;
using Microsoft.Data.SqlClient;
using StudentAggregate = LearningManagementSystem.Modules.Student.Domain.Aggregates.Student;

namespace LearningManagementSystem.Modules.Student.Infrastructure;

internal sealed class SqlStudentRepository(string connectionString) : IStudentRepository
{
    public async Task<StudentAggregate?> GetByIdAsync(
        StudentId studentId,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT StudentId, FirstName, LastName, Email, DepartmentId, StatusId
            FROM dbo.Student
            WHERE StudentId = @Id;
            """;

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = studentId.Value;

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
            return null;

        return StudentAggregate.Create(
            new StudentId(reader.GetInt32(0)),
            Name.Create(reader.GetString(1), reader.GetString(2)),
            Email.Create(reader.GetString(3)),
            (Department)reader.GetByte(4),
            (StudentStatus)reader.GetByte(5));
    }
}
