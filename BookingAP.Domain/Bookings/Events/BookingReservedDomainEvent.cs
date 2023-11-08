using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Bookings.ValueObjects;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingReservedDomainEvent(BookingId BookingId) : IDomainEvent;
}
