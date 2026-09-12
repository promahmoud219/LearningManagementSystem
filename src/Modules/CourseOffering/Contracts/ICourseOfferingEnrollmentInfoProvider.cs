using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Contracts;

public interface ICourseOfferingEnrollmentInfoProvider
{
    Task<CourseOfferingEnrollmentInfo?> GetAsync(
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken);
}
