using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Application.Bookings.GetBooking;
using BookingAP.Domain.Bookings.Repositories;
using ErrorOr;

namespace Bookify.Application.Bookings.GetBooking;

internal sealed class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, ErrorOr<BookingResponse>>
{
    private readonly IBookingRepository _bookingRepository;

    public GetBookingQueryHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<ErrorOr<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetBookingDetails<BookingResponse>(request.BookingId, cancellationToken);

        return booking;
    }
}