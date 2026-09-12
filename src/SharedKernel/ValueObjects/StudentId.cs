namespace LearningManagementSystem.SharedKernel.ValueObjects;

public readonly record struct StudentId(int Value)
{
    public static StudentId New() => new(Random.Shared.Next(1, int.MaxValue));
    public static StudentId Empty => new(0);
}
