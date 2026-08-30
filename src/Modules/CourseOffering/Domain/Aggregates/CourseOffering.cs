using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Domain.Aggregates;

public sealed class CourseOffering(
    CourseOfferingId id, 
    CourseId courseId, 
    Money price,
    EnrollmentWindow enrollmentWindow, 
    int capacity
    ) : AggregateRoot
{
    public CourseOfferingId Id { get; private set; }= id;
    public CourseId CourseId { get; private set; }= courseId;
    public Money Price { get; private set; } = price;
    public EnrollmentWindow EnrollmentWindow { get; private set; }= enrollmentWindow;
    public int Capacity { get; private set; }= capacity;
    public int CurrentEnrollmentCount { get; private set; }= 0;
    public bool IsClosedManually { get; private set; } = false;


    public static CourseOffering Create(
        CourseOfferingId id,
        Guid courseId,
        Money price,
        EnrollmentWindow enrollmentWindow,
        int maxCapacity) 
        => new (id, courseId, price, enrollmentWindow, maxCapacity);


    public bool IsFull() => CurrentEnrollmentCount >= Capacity;
  
    public void IncrementEnrollment()
    {
        if (IsFull()) 
            throw new InvalidOperationException("Course offering is full.");

        CurrentEnrollmentCount++;
    }  
    
    public bool IsEnrollmentClosed(DateTime currentDate) 
        => IsClosedManually || !EnrollmentWindow.IsOpen(currentDate);

    public void EnrollStudent()
    {
        if (IsFull())
            throw new InvalidOperationException("Course offering is full.");
        CurrentEnrollmentCount++;
    }
}
