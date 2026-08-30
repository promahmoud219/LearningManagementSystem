using MediatR;

using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;
using LearningManagementSystem.SharedKernel.ValueObjects;
using LearningManagementSystem.SharedKernel.Results;

namespace LearningManagementSystem.Modules.Enrollment.Application.Commands.RequestEnrollment;

public sealed record RequestEnrollmentCommand(
    StudentId StudentId,
    CourseOfferingId CourseOfferingId,
    string? DiscountCode) : IRequest<OperationResult<EnrollmentId>>;