namespace LearningManagementSystem.SharedKernel.Abstractions;

public abstract record DomainEvent(int EventId, DateTime OccurredOnUtc) : IDomainEvent
{
    protected DomainEvent() : this(Random.Shared.Next(1, int.MaxValue), DateTime.UtcNow)
    { 
    }
}
