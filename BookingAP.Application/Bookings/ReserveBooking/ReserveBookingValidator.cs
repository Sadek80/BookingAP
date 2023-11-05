using FluentValidation;

namespace BookingAP.Application.Bookings.ReserveBooking
{
    public sealed class ReserveBookingValidator : AbstractValidator<ReserveBookingCommand>
    {
        public ReserveBookingValidator()
        {
            RuleFor(c => c.userId).NotEmpty().NotNull();
            RuleFor(c => c.appartmentId).NotEmpty().NotNull();

            RuleFor(c => c.startDate).LessThan(c => c.endDate);
        }
    }
}
