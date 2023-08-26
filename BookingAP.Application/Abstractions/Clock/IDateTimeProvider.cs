namespace BookingAP.Application.Abstractions.Clock
{
    public interface IDateTimeProvider
    {
        DateTime UTCNow { get; }
    }
}
