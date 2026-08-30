using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;
using LearningManagementSystem.SharedKernel.Abstractions;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Domain.Events;

public sealed record EnrollmentRequested(
    EnrollmentId EnrollmentId,
    StudentId StudentId,
    CourseOfferingId CourseOfferingId,
    Money BasePrice,
    Money Discount,
    Money FinalAmount,
    string? DiscountCode = null) : DomainEvent;
