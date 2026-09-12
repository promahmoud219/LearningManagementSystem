using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Student.Contracts;

public interface IStudentEnrollmentEligibilityChecker
{
    Task<StudentEnrollmentEligibilityResult> IsEligibleAsync(
        StudentId studentId,
        CancellationToken cancellationToken);
}
