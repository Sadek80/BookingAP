using BookingAp.Contract.Bookings;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Application.Bookings.GetBooking;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Bookings.ValueObjects;
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
        var booking = await _bookingRepository.GetBookingDetails<BookingResponse>(new BookingId(request.BookingId), cancellationToken);

        if(booking is null)
        {
            return DomainError.NotFound(BookingErrors.NotFound);
        }

        return booking;
    }
}