using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;
}
