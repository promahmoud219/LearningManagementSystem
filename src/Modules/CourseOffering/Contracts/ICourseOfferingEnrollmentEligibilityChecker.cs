using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Contracts;

public interface ICourseOfferingEnrollmentEligibilityChecker
{
    Task<CourseOfferingEligibilityResult> IsEligibleAsync(
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken);
}
