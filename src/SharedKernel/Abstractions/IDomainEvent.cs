using MediatR;
namespace LearningManagementSystem.SharedKernel.Abstractions;

public interface IDomainEvent : INotification 
{
    int EventId { get; }
    DateTime OccurredOnUtc { get; }
}
