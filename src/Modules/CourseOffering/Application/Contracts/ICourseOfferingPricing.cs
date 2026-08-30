using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Contracts;

public interface ICourseOfferingPricing
{
    Task<Money> GetCurrentPriceAsync(
        CourseOfferingId courseOfferingId, 
        CancellationToken cancellationToken);
}

