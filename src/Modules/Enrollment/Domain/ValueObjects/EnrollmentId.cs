namespace LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;

public sealed record EnrollmentId(int Value)
{
    public static EnrollmentId Empty => new(0);
}