namespace LearningManagementSystem.SharedKernel.ValueObjects;

public readonly record struct StudentId(int Value)
{
    public static StudentId Empty => new(0);
}
