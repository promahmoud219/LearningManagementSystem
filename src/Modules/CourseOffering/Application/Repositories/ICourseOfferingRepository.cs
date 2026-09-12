using LearningManagementSystem.Modules.CourseOffering.Domain.Aggregates;
using LearningManagementSystem.SharedKernel.ValueObjects;
using CourseOfferingAggregate = LearningManagementSystem.Modules.CourseOffering.Domain.Aggregates.CourseOffering;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Repositories;

public interface ICourseOfferingRepository
{
    Task<CourseOfferingAggregate?> GetByIdAsync(CourseOfferingId id, CancellationToken cancellationToken);
    Task AddAsync(CourseOfferingAggregate courseOffering, CancellationToken cancellationToken);
    Task<Money> GetCurrentPriceAsync(CourseOfferingId courseOfferingId, CancellationToken cancellationToken);
}
