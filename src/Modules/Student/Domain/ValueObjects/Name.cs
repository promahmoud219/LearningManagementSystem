using System;

namespace LearningManagementSystem.Modules.Student.Domain.ValueObjects;

public sealed record Name (string firstName, string lastName)
{
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;

     
    public static Name Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 50)
            throw new ArgumentException("First name is invalid.");

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 50)
            throw new ArgumentException("Last name is invalid.");

        return new Name(firstName, lastName);
    }

    public override string ToString() => $"{FirstName} {LastName}";
}
