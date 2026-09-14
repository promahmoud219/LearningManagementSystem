using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Application.Services;

public interface IEnrollmentUniquenessChecker
{
    Task<bool> IsEnrollmentUniqueAsync(
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken);

}