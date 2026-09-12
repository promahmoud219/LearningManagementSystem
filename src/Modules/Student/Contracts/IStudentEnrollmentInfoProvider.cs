using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Student.Contracts;

public interface IStudentEnrollmentInfoProvider
{
    Task<StudentEnrollmentInfo?> GetAsync(
        StudentId studentId,
        CancellationToken cancellationToken);
}
