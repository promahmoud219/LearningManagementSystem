using LearningManagementSystem.SharedKernel.Abstractions;
using LearningManagementSystem.SharedKernel.ValueObjects;
using LearningManagementSystem.Modules.Student.Domain.ValueObjects;
using LearningManagementSystem.Modules.Student.Domain.Enums;

namespace LearningManagementSystem.Modules.Student.Domain.Aggregates;

public sealed class Student(StudentId id, Name name, Email email, Department department, StudentStatus status) : AggregateRoot
{
    
    public StudentId Id { get; } = id;  
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
}
