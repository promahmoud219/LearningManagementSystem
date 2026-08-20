using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Domain.Events;

public sealed record StudentWithdrawn(
    EnrollmentId EnrollmentId);