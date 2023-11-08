using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Bookings.ValueObjects;

namespace BookingAP.Domain.Bookings.Events
{
    public record BookingCompletedDomainEvent(BookingId BookingId) : IDomainEvent;
}
