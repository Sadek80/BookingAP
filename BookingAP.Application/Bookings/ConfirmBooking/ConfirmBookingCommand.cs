using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace Bookify.Application.Bookings.ConfirmBooking;

public sealed record ConfirmBookingCommand(Guid BookingId) : ICommand<ErrorOr<bool>>;