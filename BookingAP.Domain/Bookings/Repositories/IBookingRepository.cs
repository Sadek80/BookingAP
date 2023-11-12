using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments;
using BookingAP.Domain.Bookings.Abstractions;
using BookingAP.Domain.Bookings.ValueObjects;

namespace BookingAP.Domain.Bookings.Repositories
{
    public interface IBookingRepository : IRepository<Booking, BookingId>
    {
        Task<TResult?> GetBookingDetails<TResult>(BookingId bookingId, CancellationToken cancellationToken = default) where TResult : IBookingResponse;
        Task<bool> IsOverlappingAsync(Appartment apartment,
                                      DateRange duration,
                                      CancellationToken cancellationToken = default);
    }
}
