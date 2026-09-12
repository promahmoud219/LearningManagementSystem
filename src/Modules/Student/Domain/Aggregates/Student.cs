using LearningManagementSystem.SharedKernel.Abstractions;
using LearningManagementSystem.SharedKernel.ValueObjects;
using LearningManagementSystem.Modules.Student.Domain.ValueObjects;

namespace LearningManagementSystem.Modules.Student.Domain.Aggregates;

public sealed class Student : AggregateRoot
{
    private Student(StudentId id, Name name, Email email, Department department)
    {
        Id = id;
        Name = name;
        Email = email;
        Department = department;
    }

    public StudentId Id { get; }
    public Name Name { get; }
    public Email Email { get; }
    public Department Department { get; }

    public static Student Create(
        StudentId id, 
        Name name,
        Email email,
        Department department)
        => new(id, name, email, department);
}
