namespace LearningManagementSystem.Modules.Student.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
            throw new ArgumentException("Email is invalid.", nameof(value));

        return new Email(value.Trim().ToLowerInvariant());
    }
}
