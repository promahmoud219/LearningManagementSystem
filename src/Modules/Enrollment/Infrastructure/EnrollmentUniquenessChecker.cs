using LearningManagementSystem.Modules.Enrollment.Application.Contracts;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Infrastructure;

internal sealed class EnrollmentUniquenessChecker : IEnrollmentUniquenessChecker
{
    public Task<bool> IsEnrollmentUniqueAsync(
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}