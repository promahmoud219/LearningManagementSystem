using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Contracts;

public interface ICourseOfferingPricing
{
    Task<Money> GetCurrentPriceAsync(
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken);
}
