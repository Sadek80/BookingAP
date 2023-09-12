using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Application.Abstractions.Repositories.Bookings;
using BookingAP.Domain.Abstractions;
using ErrorOr;
using static BookingAP.Domain.Bookings.DomainErrors;

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

        if(booking is null)
        {
            return DomainError.NotFound(BookingErrors.NotFound);
        }

        return booking;
    }
}