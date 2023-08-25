using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Reviews.Events
{
    public record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
}
