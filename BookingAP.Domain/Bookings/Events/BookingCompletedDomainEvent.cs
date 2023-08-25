using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;
}
