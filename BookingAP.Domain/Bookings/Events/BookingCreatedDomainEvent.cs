using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingCreatedDomainEvent(Guid BookingId) : IDomainEvent;
}
