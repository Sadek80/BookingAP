using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments;
using BookingAP.Domain.Bookings.ValueObjects;

namespace BookingAP.Domain.Bookings.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<bool> IsOverlappingAsync(Appartment apartment,
                                      DateRange duration,
                                      CancellationToken cancellationToken = default);
    }
}
