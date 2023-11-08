using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Bookings.ValueObjects
{
    public record BookingId(Guid Value) : IEntityId<BookingId>
    {
        public static BookingId New => new BookingId(Guid.NewGuid());
    }
}
