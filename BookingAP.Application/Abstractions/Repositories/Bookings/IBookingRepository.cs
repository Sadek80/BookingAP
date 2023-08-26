using BookingAP.Domain.Appartments;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Bookings.ValueObjects;

namespace BookingAP.Application.Abstractions.Repositories.Bookings
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<TResult> GetBookingDetails<TResult>(Guid bookingId, CancellationToken cancellationToken = default);
        Task<bool> IsOverlappingAsync(Appartment apartment,
                                      DateRange duration,
                                      CancellationToken cancellationToken = default);
    }
}
