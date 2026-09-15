using LearningManagementSystem.Modules.Student.Contracts;
using LearningManagementSystem.Modules.Student.Application.Repositories;
using LearningManagementSystem.SharedKernel.ValueObjects;
using LearningManagementSystem.Modules.Student.Domain.Enums;

namespace LearningManagementSystem.Modules.Student.Application.Services;

internal sealed class StudentEnrollmentEligibilityChecker(IStudentRepository repository)
    : IStudentEnrollmentEligibilityChecker
{
    public async Task<StudentEnrollmentEligibilityResult> IsEligibleAsync(
        StudentId studentId,
        CancellationToken cancellationToken)
    {
        if (studentId.Value <= 0)
            return StudentEnrollmentEligibilityResult.InvalidStudentId;

        var student = await repository.GetByIdAsync(studentId, cancellationToken);

        if (student is null)
            return StudentEnrollmentEligibilityResult.StudentNotFound;

        return student.Status == StudentStatus.Active
            ? StudentEnrollmentEligibilityResult.Eligible
            : StudentEnrollmentEligibilityResult.StudentNotActive;
    }
}
