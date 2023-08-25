using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingCanceledDomianEvent(Guid BookingId) : IDomainEvent;
}
