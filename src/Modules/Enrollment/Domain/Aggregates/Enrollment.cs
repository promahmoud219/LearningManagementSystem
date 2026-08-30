using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;
using LearningManagementSystem.Modules.Enrollment.Domain.Events;
using LearningManagementSystem.Modules.Enrollment.Domain.Enums;

using LearningManagementSystem.SharedKernel.Abstractions;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Domain.Aggregates;

public sealed class Enrollment(
    EnrollmentId id, 
    StudentId studentId, 
    CourseOfferingId courseOfferingId, 
    DateTime enrollmentDate, 
    Money basePrice, 
    Money discount, 
    string? discountCode): AggregateRoot
{
    public EnrollmentId Id { get; private set; }= id;
    public StudentId StudentId { get; private set; }= studentId;
    public CourseOfferingId CourseOfferingId { get; private set; }= courseOfferingId;
    public EnrollmentStatus Status { get; private set; }
    public DateTime EnrollmentDate { get; private set; }= enrollmentDate;
    public Money BasePrice { get; private set; }= basePrice;
    public Money Discount { get; private set; }= discount;
    public Money FinalAmount { get; private set; }= basePrice - discount;
    public string? DiscountCode { get; private set; }= discountCode;
    public EnrollmentStatus Status { get; private set; } = EnrollmentStatus.Requested;

    public static Enrollment Create(
        EnrollmentId id,
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        DateTime enrollmentDate,
        Money basePrice,
        Money discount,
        string? discountCode)
    {
        var enrollment = new Enrollment(
            id, studentId, courseOfferingId, enrollmentDate, basePrice, discount, discountCode);

        enrollment.RaiseDomainEvent(
            new EnrollmentRequested(
                id, studentId, courseOfferingId, basePrice, discount, enrollment.FinalAmount, discountCode));

        return enrollment;
    }

    public void Approve()
    {
        if (Status != EnrollmentStatus.Requested)
            throw new InvalidOperationException("Only requested enrollments can be approved.");

        Status = EnrollmentStatus.Approved;
        RaiseDomainEvent(new EnrollmentApproved(Id));
    }

    public void Reject()
    {
        if (Status != EnrollmentStatus.Requested)
            throw new InvalidOperationException("Only requested enrollments can be rejected.");

        Status = EnrollmentStatus.Rejected;
        RaiseDomainEvent(new EnrollmentRejected(Id));
    }

    public void Withdraw()
    {
        if (Status != EnrollmentStatus.Approved)
            throw new InvalidOperationException("Only approved enrollments can be withdrawn.");

        Status = EnrollmentStatus.Withdrawn;
        RaiseDomainEvent(new EnrollmentWithdrawn(Id));
    }
}