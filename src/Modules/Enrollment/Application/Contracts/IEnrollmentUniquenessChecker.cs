using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Application.Contracts;

public interface IEnrollmentUniquenessChecker
{
    Task<bool> IsEnrollmentUniqueAsync(
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken);

}