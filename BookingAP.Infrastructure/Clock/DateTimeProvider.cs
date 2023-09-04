using BookingAP.Application.Abstractions.Clock;

namespace BookingAP.Infrastructure.Clock
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UTCNow => DateTime.UtcNow;
    }
}
