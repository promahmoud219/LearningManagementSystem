using LearningManagementSystem.SharedKernel.ValueObjects;
using LearningManagementSystem.Modules.CourseOffering.Application.Contracts;
using LearningManagementSystem.Modules.CourseOffering.Domain.Repositories;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Services;

internal sealed class CourseOfferingEnrollmentEligibilityChecker(ICourseOfferingRepository repository) 
    : ICourseOfferingEnrollmentEligibilityChecker
{
    private readonly ICourseOfferingRepository _repository = repository;

    public async Task<CourseOfferingEligibilityResult> CheckEligibilityAsync(
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken)
    {
        var courseOffering = await _repository.GetByIdAsync(courseOfferingId, cancellationToken);

        if (courseOffering is null)
            throw new InvalidOperationException($"Course offering with ID '{courseOfferingId.Value}' " +
                                                $"  was not found.");


        if (courseOffering.IsClosed(DateTime.UtcNow))
            return CourseOfferingEligibilityResult.Closed;


        if (courseOffering.IsFull())
            return CourseOfferingEligibilityResult.Full;


        return CourseOfferingEligibilityResult.Eligible;
    }
}