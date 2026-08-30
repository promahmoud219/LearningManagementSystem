using MediatR;
namespace LearningManagementSystem.SharedKernel.Abstractions;

public interface IDomainEvent : INotification 
{
    Guid EventId { get; }
    DateTime OccurredOnUtc { get; }
}
