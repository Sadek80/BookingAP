using BookingAP.Domain.Appartments;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Bookings.ValueObjects;

namespace BookingAP.Infrastructure.Repositories.Bookings
{
    internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TResult> GetBookingDetails<TResult>(Guid bookingId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOverlappingAsync(Appartment apartment, DateRange duration, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
