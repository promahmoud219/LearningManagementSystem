using LearningManagementSystem.Modules.Enrollment.Domain.Aggregates;
using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Domain.Repositories;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetByIdAsync(EnrollmentId id, CancellationToken cancellationToken);

    Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken);
}