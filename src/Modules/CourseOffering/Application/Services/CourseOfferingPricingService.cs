using LearningManagementSystem.Modules.CourseOffering.Contracts;
using LearningManagementSystem.Modules.CourseOffering.Application.Repositories;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Services;

internal sealed class CourseOfferingPricingService(ICourseOfferingRepository repository)
    : ICourseOfferingPricing
{
    public async Task<Money> GetCurrentPriceAsync(
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken)
    {
        var courseOfferingPrice = await repository.GetCurrentPriceAsync(courseOfferingId, cancellationToken)
            ?? throw new InvalidOperationException(
                $"Course offering with ID '{courseOfferingId.Value}' was not found.");

        return courseOfferingPrice;
    }
}
