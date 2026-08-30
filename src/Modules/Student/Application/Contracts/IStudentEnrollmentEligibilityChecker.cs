using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Student.Application.Contracts;

public interface IStudentEnrollmentEligibilityChecker
{
    Task<StudentEnrollmentEligibilityResult> IsEligibleAsync(
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken);
}