using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments;
using BookingAP.Domain.Bookings.ValueObjects;

namespace BookingAP.Domain.Bookings.Repositories
{
    public interface IBookingRepository : IRepository<Booking, BookingId>
    {
        Task<TResult?> GetBookingDetails<TResult>(BookingId bookingId, CancellationToken cancellationToken = default);
        Task<bool> IsOverlappingAsync(Appartment apartment,
                                      DateRange duration,
                                      CancellationToken cancellationToken = default);
    }
}
