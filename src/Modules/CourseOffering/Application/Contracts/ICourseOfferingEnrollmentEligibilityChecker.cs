using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Contracts;

public interface ICourseOfferingEnrollmentEligibilityChecker
{
    Task<CourseOfferingEligibilityResult> CheckEligibilityAsync(
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken);
}