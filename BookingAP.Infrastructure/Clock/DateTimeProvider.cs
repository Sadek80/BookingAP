using BookingAP.Application.Abstractions.Clock;

namespace BookingAP.Infrastructure.Clock
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UTCNow => DateTime.UtcNow;
    }
}
