using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace Bookify.Application.Bookings.RejectBooking;

public sealed record RejectBookingCommand(Guid BookingId) : ICommand<ErrorOr<bool>>;