namespace LearningManagementSystem.Modules.Student.Contracts;

public enum StudentEnrollmentEligibilityResult : byte
{
    Eligible = 0,
    InvalidStudentId = 1,
    StudentNotFound = 2,
    StudentNotActive = 3,
}
