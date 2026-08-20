namespace LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;

public sealed record EnrollmentId(Guid Value)
{
    public static EnrollmentId Create() => new(Guid.NewGuid());
}