using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;


namespace LearningManagementSystem.Modules.Enrollment.Domain.Events;

public sealed record EnrollmentRequested(
    EnrollmentRequestId EnrollmentRequestId,
    StudentId StudentId,
    CourseOfferingId CourseOfferingId,
    decimal Amount,
    string Currency,
    DateTime RequestedAt);