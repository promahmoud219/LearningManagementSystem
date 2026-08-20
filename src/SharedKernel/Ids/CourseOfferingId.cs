using System;

namespace LearningManagementSystem.SharedKernel.Ids;

public readonly record struct CourseOfferingId(Guid Value)
{
    public static CourseOfferingId New() => new(Guid.NewGuid());
    public static CourseOfferingId Empty => new(Guid.Empty);
}
