using MediatR;

namespace LearningManagementSystem.Modules.CourseOffering.Application.Queries.GetAllCourseOfferings;

public sealed record GetAllCourseOfferingsQuery
    : IRequest<IReadOnlyList<CourseOfferingListItem>>;
