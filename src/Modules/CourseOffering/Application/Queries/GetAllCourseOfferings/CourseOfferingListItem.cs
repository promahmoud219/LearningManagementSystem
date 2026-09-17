namespace LearningManagementSystem.Modules.CourseOffering.Application.Queries.GetAllCourseOfferings;

public sealed record CourseOfferingListItem(
    int CourseOfferingId,
    int CourseId,
    string CourseName,
    int DepartmentId,
    string DepartmentName,
    byte TermId,
    short StudyYear,
    decimal Price,
    int Capacity,
    int CurrentEnrollmentCount);