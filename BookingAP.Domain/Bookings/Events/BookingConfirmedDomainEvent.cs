using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;
}
