using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Reviews.ValueObjects;

namespace BookingAP.Domain.Reviews.Events
{
    public record ReviewCreatedDomainEvent(ReviewId ReviewId) : IDomainEvent;
}
