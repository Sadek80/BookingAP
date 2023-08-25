using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;
}
