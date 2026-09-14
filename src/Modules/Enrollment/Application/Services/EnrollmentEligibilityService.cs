using LearningManagementSystem.Modules.Enrollment.Domain.Enums;

using LearningManagementSystem.Modules.CourseOffering.Contracts;

using LearningManagementSystem.Modules.Student.Contracts;

using LearningManagementSystem.SharedKernel.ValueObjects; 


namespace LearningManagementSystem.Modules.Enrollment.Application.Services;

internal sealed class EnrollmentEligibilityService (
    IStudentEnrollmentEligibilityChecker studentEligibility,
    ICourseOfferingEnrollmentEligibilityChecker courseOfferingEligibility,
    ICourseOfferingEnrollmentInfoProvider courseOfferingInfoProvider,
    IStudentEnrollmentInfoProvider studentInfoProvider,
    IEnrollmentUniquenessChecker uniquenessChecker) 
    
{
    private readonly IStudentEnrollmentEligibilityChecker _studentEligibility = studentEligibility;
    private readonly ICourseOfferingEnrollmentEligibilityChecker _courseOfferingEligibility = courseOfferingEligibility;
    private readonly IStudentEnrollmentInfoProvider _studentInfoProvider = studentInfoProvider;
    private readonly ICourseOfferingEnrollmentInfoProvider _courseOfferingInfoProvider = courseOfferingInfoProvider;
    private readonly IEnrollmentUniquenessChecker _uniquenessChecker = uniquenessChecker;


    public async Task<EligibilityResult> IsEligibleAsync(
        StudentId studentId,
        CourseOfferingId courseOfferingId,
        CancellationToken cancellationToken)
    {
        var studentResult = await _studentEligibility.IsEligibleAsync(
            studentId, cancellationToken);

        if (studentResult == StudentEnrollmentEligibilityResult.InvalidStudentId)
            return EligibilityResult.InvalidStudentId;
        
        if (studentResult == StudentEnrollmentEligibilityResult.StudentNotFound)
            return EligibilityResult.StudentNotFound;

        if (studentResult == StudentEnrollmentEligibilityResult.StudentNotActive)
            return EligibilityResult.StudentNotActive;


        var courseResult = await _courseOfferingEligibility.IsEligibleAsync(
            courseOfferingId, cancellationToken);

        if (courseResult == CourseOfferingEligibilityResult.NotFound)
            return EligibilityResult.CourseOfferingNotFound; 

        if (courseResult == CourseOfferingEligibilityResult.Full)
            return EligibilityResult.CapacityExceeded;


        var studentInfo = await _studentInfoProvider.GetAsync(
            studentId, cancellationToken);

        var courseOfferingInfo = await _courseOfferingInfoProvider.GetAsync(
            courseOfferingId, cancellationToken);

        if (studentInfo is null || courseOfferingInfo is null)
            return studentInfo is null ? EligibilityResult.StudentNotFound : EligibilityResult.CourseOfferingNotFound;

        if (studentInfo.Department != courseOfferingInfo.Department)
            return EligibilityResult.DepartmentMismatch; 

        var isEnrollmentUnique = await _uniquenessChecker.IsEnrollmentUniqueAsync(
            studentId, courseOfferingId, cancellationToken);

        if (!isEnrollmentUnique)
            return EligibilityResult.DuplicateEnrollment;

        return EligibilityResult.Eligible;
    }
 
}
