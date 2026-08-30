using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;
using LearningManagementSystem.SharedKernel.Abstractions;

namespace LearningManagementSystem.Modules.Enrollment.Domain.Events;

public sealed record EnrollmentRejected(
    EnrollmentId EnrollmentId) : DomainEvent;