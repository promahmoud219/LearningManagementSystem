namespace LearningManagementSystem.SharedKernel.ValueObjects;

public readonly record struct StudentId(Guid Value)
{
    public static StudentId New() => new(Guid.NewGuid());
    public static StudentId Empty => new(Guid.Empty);
}
