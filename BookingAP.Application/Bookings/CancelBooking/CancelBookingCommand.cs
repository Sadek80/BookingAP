using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace Bookify.Application.Bookings.CancelBooking;

public record CancelBookingCommand(Guid BookingId) : ICommand<ErrorOr<bool>>;