namespace LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;

public sealed record EnrollmentId(int Value)
{
    public static EnrollmentId Create() => new(Random.Shared.Next(1, int.MaxValue));
}