namespace LearningManagementSystem.SharedKernel.Results;

public sealed record OperationResult<T>(
    OperationStatus Status,
    T? Output = default,
    Error? Error = null
)
{
    public static OperationResult<T> Success(T output) =>
        new(OperationStatus.Success, output, null);

    public static OperationResult<T> Failure(Error error) =>
        new(OperationStatus.Failure, default, error);

    public static OperationResult<T> ValidationError(Error error) =>
        new(OperationStatus.ValidationError, default, error);

    public static OperationResult<T> NotFound(Error error) =>
        new(OperationStatus.NotFound, default, error);

    public static OperationResult<T> NeedsConfirmation(Error error) =>
        new(OperationStatus.NeedsConfirmation, default, error);
    
    public static OperationResult<T> BusinessRuleError(Error error) =>
        new(OperationStatus.BusinessRuleError, default, error);
}

public sealed record OperationResult(
    OperationStatus Status,
    Error? Error = null
)
{
    public static OperationResult Success() =>
        new(OperationStatus.Success, null);

    public static OperationResult Failure(Error error) =>
        new(OperationStatus.Failure, error);

    public static OperationResult ValidationError(Error error) =>
        new(OperationStatus.ValidationError, error);

    public static OperationResult NotFound(Error error) =>
        new(OperationStatus.NotFound, error);

    public static OperationResult NeedsConfirmation(Error error) =>
        new(OperationStatus.NeedsConfirmation, error);

    public static OperationResult BusinessRuleError(Error error) =>
        new(OperationStatus.BusinessRuleError, error);
}