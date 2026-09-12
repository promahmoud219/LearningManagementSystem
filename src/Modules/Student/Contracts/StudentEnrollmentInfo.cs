using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Student.Contracts;

public sealed record StudentEnrollmentInfo(
    StudentId Id,
    Department Department);
