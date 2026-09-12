using LearningManagementSystem.Modules.Student.Contracts;
using LearningManagementSystem.Modules.Student.Application.Repositories;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Student.Application.Services;

internal sealed class StudentEnrollmentInfoProvider(IStudentRepository repository)
    : IStudentEnrollmentInfoProvider
{
    public async Task<StudentEnrollmentInfo?> GetAsync(
        StudentId studentId,
        CancellationToken cancellationToken)
    {
        var student = await repository.GetByIdAsync(studentId, cancellationToken);

        return student is null
            ? null
            : new StudentEnrollmentInfo(student.Id, student.Department);
    }
}
