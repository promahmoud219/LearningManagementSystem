namespace LearningManagementSystem.Modules.Enrollment.Domain.Enums;

public enum EligibilityResult : byte
{
    Eligible = 0,
    PrerequisitesNotMet = 1,
    DuplicateEnrollment = 2,
    CapacityExceeded = 3,
    DepartmentMismatch = 4,
    CourseOfferingNotFound = 5,
    InvalidStudentId = 6,
    StudentNotFound = 7,
    StudentNotActive = 8
}