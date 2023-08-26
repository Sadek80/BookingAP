using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace Bookify.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : IQuery<ErrorOr<BookingResponse>>;