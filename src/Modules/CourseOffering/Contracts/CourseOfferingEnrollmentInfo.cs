using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.CourseOffering.Contracts;

public sealed record CourseOfferingEnrollmentInfo(
    CourseOfferingId Id,
    Department Department);
