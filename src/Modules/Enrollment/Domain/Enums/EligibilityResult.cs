namespace LearningManagementSystem.Modules.Enrollment.Domain.Enums;

public enum EligibilityResult
{
    Eligible = 0,
    PrerequisitesNotMet = 1,
    DuplicateEnrollment = 2,
    CapacityExceeded = 3,
    OutsideEnrollmentWindow = 4
}