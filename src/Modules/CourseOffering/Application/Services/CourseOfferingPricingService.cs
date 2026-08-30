using LearningManagementSystem.Modules.CourseOffering.Application.Contracts;
using LearningManagementSystem.Modules.CourseOffering.Domain.Repositories;  
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Services;

internal sealed class CourseOfferingPricingService(ICourseOfferingRepository repository) : ICourseOfferingPricing
{
    private readonly ICourseOfferingRepository _repository = repository;

    public async Task<Money> GetCurrentPriceAsync(
        CourseOfferingId courseOfferingId, 
        CancellationToken cancellationToken)
    {
        var courseOffering = await _repository.GetByIdAsync(courseOfferingId, cancellationToken);

        if (courseOffering is null)
            throw new InvalidOperationException($"Course offering with ID '{courseOfferingId.Value}' " +
                                                $"was not found.");

        return courseOffering.Price; 
    }
}