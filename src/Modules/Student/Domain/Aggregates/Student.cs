using LearningManagementSystem.SharedKernel.Abstractions;
using LearningManagementSystem.SharedKernel.ValueObjects;

using LearningManagementSystem.Modules.Student.Domain.ValueObjects;
using LearningManagementSystem.Modules.Student.Domain.Enums;

namespace LearningManagementSystem.Modules.Student.Domain.Aggregates;

public sealed class Student(
    StudentId id,
    Name name,
    Email email,
    Department department,
    StudentStatus status) : AggregateRoot
{
    public StudentId Id { get; private set; } = id;
    public Name Name { get; } = name;
    public Email Email { get; } = email;
    public Department Department { get; } = department;
    public StudentStatus Status { get; } = status;

    public static Student Create(
        StudentId id,
        Name name,
        Email email,
        Department department,
        StudentStatus status)
        => new(id, name, email, department, status);

    public void AssignId(StudentId studentId)
    {
        if (Id != StudentId.Empty)
            throw new InvalidOperationException(
                "Student ID has already been assigned.");

        if (studentId == StudentId.Empty)
            throw new ArgumentException(
                "Student ID cannot be empty.",
                nameof(studentId));

        Id = studentId;
    }
}

