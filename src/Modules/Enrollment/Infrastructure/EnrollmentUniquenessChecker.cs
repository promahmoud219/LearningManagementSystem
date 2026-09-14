using System.Data;
using LearningManagementSystem.Modules.Enrollment.Application.Services;
using LearningManagementSystem.SharedKernel.ValueObjects;
using Microsoft.Data.SqlClient;

namespace LearningManagementSystem.Modules.Enrollment.Infrastructure;

internal sealed class EnrollmentUniquenessChecker(string connectionString) : IEnrollmentUniquenessChecker
{
    public async Task<bool> IsEnrollmentUniqueAsync(
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT CASE WHEN EXISTS
            (
                SELECT 1
                FROM dbo.Enrollment
                WHERE StudentId = @StudentId
                  AND CourseOfferingId = @CourseOfferingId
                  AND StatusId IN (0, 1)
            ) THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END;
            """;

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@StudentId", SqlDbType.Int).Value = studentId.Value;
        command.Parameters.Add("@CourseOfferingId", SqlDbType.Int).Value = courseOfferingId.Value;

        return (bool)(await command.ExecuteScalarAsync(cancellationToken))!;
    }
}
