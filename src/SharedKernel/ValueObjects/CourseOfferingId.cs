namespace LearningManagementSystem.SharedKernel.ValueObjects;

public readonly record struct CourseOfferingId(int Value)
{
    public static CourseOfferingId Empty => new(0);
}
