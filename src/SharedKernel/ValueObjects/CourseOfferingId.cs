namespace LearningManagementSystem.SharedKernel.ValueObjects;

public readonly record struct CourseOfferingId(int Value)
{
    public static CourseOfferingId New() => new(Random.Shared.Next(1, int.MaxValue));
    public static CourseOfferingId Empty => new(0);
}
