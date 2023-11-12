using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Bookings.ValueObjects;
using ErrorOr;

namespace BookingAP.Application.Bookings.ReserveBooking
{
    public sealed record ReserveBookingCommand(string userIdentityId,
                                               Guid appartmentId,
                                               DateOnly startDate,
                                               DateOnly endDate) : ICommand<ErrorOr<BookingId>>;
}
