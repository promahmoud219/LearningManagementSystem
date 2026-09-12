using LearningManagementSystem.Modules.Student.Domain.Aggregates;
using LearningManagementSystem.SharedKernel.ValueObjects;
using StudentAggregate = LearningManagementSystem.Modules.Student.Domain.Aggregates.Student;

namespace LearningManagementSystem.Modules.Student.Application.Repositories;

public interface IStudentRepository
{
    Task<StudentAggregate?> GetByIdAsync(StudentId studentId, CancellationToken cancellationToken);
}
