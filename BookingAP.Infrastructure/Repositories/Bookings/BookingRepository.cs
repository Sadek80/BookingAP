using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookingAP.Domain.Appartments;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Bookings.Enums;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Bookings.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BookingAP.Infrastructure.Repositories.Bookings
{
    internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        private static readonly BookingStatus[] ActiveBookingStatuses =
        {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed
        };


        public BookingRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TResult?> GetBookingDetails<TResult>(Guid bookingId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Booking>()
                                   .AsNoTracking()
                                   .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                                   .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsOverlappingAsync(Appartment apartment, DateRange duration, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                      .Set<Booking>()
                      .AnyAsync(
                          booking =>
                              booking.Id == apartment.Id &&
                              booking.Duration.Start <= duration.End &&
                              booking.Duration.End >= duration.Start &&
                              ActiveBookingStatuses.Contains(booking.Status),
                              cancellationToken);
        }
    }
}
