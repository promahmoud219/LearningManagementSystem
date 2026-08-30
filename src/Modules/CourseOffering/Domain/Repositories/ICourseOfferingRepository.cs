using LearningManagementSystem.Modules.CourseOffering.Domain.Aggregates;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Domain.Repositories;

public interface ICourseOfferingRepository
{
    Task<CourseOffering?> GetByIdAsync(CourseOfferingId id, CancellationToken cancellationToken);
    Task AddAsync(CourseOffering courseOffering, CancellationToken cancellationToken);
}