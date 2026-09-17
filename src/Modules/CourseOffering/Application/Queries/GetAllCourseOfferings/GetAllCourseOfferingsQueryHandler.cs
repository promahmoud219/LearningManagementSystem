using MediatR;
using LearningManagementSystem.Modules.CourseOffering.Application.Repositories;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Queries.GetAllCourseOfferings;

internal sealed class GetAllCourseOfferingsQueryHandler(
    ICourseOfferingRepository repository)
    : IRequestHandler<
        GetAllCourseOfferingsQuery,
        IReadOnlyList<CourseOfferingListItem>>
{
    public async Task<IReadOnlyList<CourseOfferingListItem>> Handle(
        GetAllCourseOfferingsQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);
    }
}