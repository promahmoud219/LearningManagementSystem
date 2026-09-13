using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;
using EnrollmentAggregate = LearningManagementSystem.Modules.Enrollment.Domain.Aggregates.Enrollment;

namespace LearningManagementSystem.Modules.Enrollment.Application.Repositories;

public interface IEnrollmentRepository
{
    Task<EnrollmentId> AddAsync(EnrollmentAggregate enrollment, CancellationToken cancellationToken);
}