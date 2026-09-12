using LearningManagementSystem.Modules.Student.Contracts;
using LearningManagementSystem.Modules.Student.Application.Repositories;
using LearningManagementSystem.SharedKernel.ValueObjects;

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

        return await repository.GetByIdAsync(studentId, cancellationToken) is null
            ? StudentEnrollmentEligibilityResult.StudentNotFound
            : StudentEnrollmentEligibilityResult.Eligible;
    }
}
