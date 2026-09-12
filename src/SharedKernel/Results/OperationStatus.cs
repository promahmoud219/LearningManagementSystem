namespace LearningManagementSystem.SharedKernel.Results;

public enum OperationStatus : byte
{
    Success,
    Failure,
    NotFound,
    ValidationError,
    NeedsConfirmation,
    BusinessRuleError
}