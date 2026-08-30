using LearningManagementSystem.Modules.Enrollment.Application.Contracts;
using LearningManagementSystem.Modules.Enrollment.Domain.Enums;
using LearningManagementSystem.SharedKernel.ValueObjects; 

using LearningManagementSystem.Modules.CourseOffering.Application.Contracts;
using LearningManagementSystem.Modules.Student.Application.Contracts;

namespace LearningManagementSystem.Modules.Enrollment.Application.Services;

internal sealed class EnrollmentEligibilityService
{
    private readonly IStudentEnrollmentEligibilityChecker _studentEligibility;
    private readonly ICourseOfferingEnrollmentEligibilityChecker _courseOfferingEligibility;
    private readonly IEnrollmentUniquenessChecker _uniquenessChecker;

    public EnrollmentEligibilityService(
        IStudentEnrollmentEligibilityChecker studentEligibility,
        ICourseOfferingEnrollmentEligibilityChecker  courseOfferingEligibility,
        IEnrollmentUniquenessChecker uniquenessChecker)
    {
        _studentEligibility = studentEligibility;
        _courseOfferingEligibility = courseOfferingEligibility;
        _uniquenessChecker = uniquenessChecker;
    }

    public async Task<EligibilityResult> CheckEligibilityAsync(
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken)
    {
        var studentResult = await _studentEligibility.IsEligibleAsync(
            studentId, courseOfferingId, cancellationToken);

        if (studentResult == StudentEnrollmentEligibilityResult.PrerequisitesNotMet)
            return EligibilityResult.PrerequisitesNotMet;

        var courseResult = await _courseOfferingEligibility.CheckEligibilityAsync(
            courseOfferingId, cancellationToken);

        if (courseResult == CourseOfferingEligibilityResult.Closed)
            return EligibilityResult.OutsideEnrollmentWindow;

        if (courseResult == CourseOfferingEligibilityResult.Full)
            return EligibilityResult.CapacityExceeded;

        var alreadyEnrolled = await _uniquenessChecker.IsEnrollmentUniqueAsync(
            studentId, courseOfferingId, cancellationToken);

        if (alreadyEnrolled)
            return EligibilityResult.DuplicateEnrollment;

        return EligibilityResult.Eligible;
    }
}