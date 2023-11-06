using BookingAp.Contract.Bookings;
using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace BookingAP.Application.Bookings.GetBooking
{
    public sealed record GetBookingQuery(Guid BookingId) : IQuery<ErrorOr<BookingResponse>>;
}