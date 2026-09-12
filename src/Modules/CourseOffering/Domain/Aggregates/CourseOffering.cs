using LearningManagementSystem.SharedKernel.Abstractions;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Domain.Aggregates;

public sealed class CourseOffering : AggregateRoot
{
    private CourseOffering(
        CourseOfferingId id,
        CourseId courseId,
        TermId termId,
        short studyYear,
        Department department,
        Money price,
        int capacity,
        int currentEnrollmentCount)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        Id = id;
        CourseId = courseId;
        TermId = termId;
        StudyYear = studyYear;
        Department = department;
        Price = price;
        Capacity = capacity;
        CurrentEnrollmentCount = currentEnrollmentCount;
    }

    public CourseOfferingId Id { get; private set; }
    public CourseId CourseId { get; private set; }
    public TermId TermId { get; private set; }
    public short StudyYear { get; private set; }
    public Department Department { get; private set; }
    public Money Price { get; private set; }
    public int Capacity { get; private set; }
    public int CurrentEnrollmentCount { get; private set; }


    public static CourseOffering Create(
        CourseOfferingId id,
        CourseId courseId,
        TermId termId,
        short studyYear,
        Money price,
        Department department,
        int capacity)
        => new(id, courseId, termId, studyYear, department, price, capacity, 0);

    public static CourseOffering Rehydrate(
        CourseOfferingId id,
        CourseId courseId,
        TermId termId,
        short studyYear,
        Department department,
        Money price,
        int capacity,
        int currentEnrollmentCount)
        => new(id, courseId, termId, studyYear, department, price, capacity,
            currentEnrollmentCount);


    public bool IsFull() => CurrentEnrollmentCount >= Capacity;
  
    public void IncrementEnrollment()
    {
        if (IsFull()) 
            throw new InvalidOperationException("Course offering is full.");

        CurrentEnrollmentCount++;
    }  
    
    public void EnrollStudent()
    {
        if (IsFull())
            throw new InvalidOperationException("Course offering is full.");
        CurrentEnrollmentCount++;
    }
}
