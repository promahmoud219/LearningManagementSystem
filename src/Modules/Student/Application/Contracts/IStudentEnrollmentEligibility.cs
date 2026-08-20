public interface IStudentEnrollmentEligibility
{
    Task<bool> IsEligibleAsync(
        StudentId studentId,
	CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken);
}
