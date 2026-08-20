using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;
using LearningManagementSystem.SharedKernel.Ids;


namespace LearningManagementSystem.Modules.Enrollment.Domain.Aggregates;
public sealed class Enrollment
{
    public EnrollmentId Id { get; private set; }

    public StudentId StudentId { get; private set; }

    public CourseOfferingId CourseOfferingId { get; private set; }

    public EnrollmentStatus Status { get; private set; }

    public DateTime EnrollmentDate { get; private set; } = DateTime.UtcNow;

    private Enrollment(
        EnrollmentId id,
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        EnrollmentDate enrollmentDate)
    {
        Id = id;
        StudentId = studentId;
        CourseOfferingId = courseOfferingId;
        EnrollmentDate = enrollmentDate;
        Status = EnrollmentStatus.Requested;
    }

    public static Enrollment Create(
        EnrollmentId id,
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        EnrollmentDate enrollmentDate)
    {
        var enrollment = new Enrollment(
            id,
            studentId,
            courseOfferingId,
            enrollmentDate);

        enrollment.AddDomainEvent(
            new EnrollmentRequested(
                id,
                studentId,
                courseOfferingId));

        return enrollment;
    }

    public void Approve()
    {
        if (Status != EnrollmentStatus.Requested)
            throw new InvalidOperationException(
                "Only requested enrollments can be approved.");

        Status = EnrollmentStatus.Approved;


        AddDomainEvent(new EnrollmentApproved(Id));

    }

    public void Reject()
    {
        if (Status != EnrollmentStatus.Requested)
            throw new InvalidOperationException(
                "Only requested enrollments can be rejected.");

        Status = EnrollmentStatus.Rejected;
    }

    public void Withdraw()
    {
        if (Status != EnrollmentStatus.Approved)
            throw new InvalidOperationException(
                "Only approved enrollments can be withdrawn.");

        Status = EnrollmentStatus.Withdrawn;
    }
}