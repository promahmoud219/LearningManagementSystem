using LearningManagementSystem.Modules.CourseOffering.Contracts;
using LearningManagementSystem.Modules.CourseOffering.Application.Repositories;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Services;

internal sealed class CourseOfferingEnrollmentEligibilityChecker(ICourseOfferingRepository repository)
    : ICourseOfferingEnrollmentEligibilityChecker
{
    public async Task<CourseOfferingEligibilityResult> IsEligibleAsync(
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken)
    {
        if (courseOfferingId.Value <= 0)
            return CourseOfferingEligibilityResult.NotFound;

        var courseOffering = await repository.GetByIdAsync(courseOfferingId, cancellationToken);

        if (courseOffering is null)
            return CourseOfferingEligibilityResult.NotFound;

        return courseOffering.IsFull()
            ? CourseOfferingEligibilityResult.Full
            : CourseOfferingEligibilityResult.Eligible;
    }
}
