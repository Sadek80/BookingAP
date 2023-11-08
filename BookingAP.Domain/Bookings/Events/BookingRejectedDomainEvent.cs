using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Bookings.ValueObjects;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingRejectedDomainEvent(BookingId BookingId) : IDomainEvent;
}
