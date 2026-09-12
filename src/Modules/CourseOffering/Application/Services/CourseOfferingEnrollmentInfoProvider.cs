using LearningManagementSystem.Modules.CourseOffering.Contracts;
using LearningManagementSystem.Modules.CourseOffering.Application.Repositories;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Services;

internal sealed class CourseOfferingEnrollmentInfoProvider(ICourseOfferingRepository repository)
    : ICourseOfferingEnrollmentInfoProvider
{
    public async Task<CourseOfferingEnrollmentInfo?> GetAsync(
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken)
    {
        var courseOffering = await repository.GetByIdAsync(courseOfferingId, cancellationToken);

        return courseOffering is null
            ? null
            : new CourseOfferingEnrollmentInfo(courseOffering.Id, courseOffering.Department);
    }
}
