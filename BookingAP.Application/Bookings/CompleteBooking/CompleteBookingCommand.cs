using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace Bookify.Application.Bookings.CompleteBooking;

public record CompleteBookingCommand(Guid BookingId) : ICommand<ErrorOr<bool>>;