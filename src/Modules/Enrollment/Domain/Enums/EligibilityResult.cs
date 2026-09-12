namespace LearningManagementSystem.Modules.Enrollment.Domain.Enums;

public enum EligibilityResult : byte
{
    Eligible = 0,
    PrerequisitesNotMet = 1,
    DuplicateEnrollment = 2,
    CapacityExceeded = 3,
    OutsideEnrollmentWindow = 4,
    DepartmentMismatch = 5,
    InvalidStudent = 6,
    InvalidCourseOffering = 7
}
