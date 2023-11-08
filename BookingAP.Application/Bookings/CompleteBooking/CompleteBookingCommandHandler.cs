using BookingAP.Application.Abstractions.Clock;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Bookings.ValueObjects;
using ErrorOr;
using static BookingAP.Domain.Bookings.DomainErrors;

namespace Bookify.Application.Bookings.CompleteBooking;

internal sealed class CompleteBookingCommandHandler : ICommandHandler<CompleteBookingCommand, ErrorOr<bool>>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteBookingCommandHandler(
        IDateTimeProvider dateTimeProvider,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(CompleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(new BookingId(request.BookingId), cancellationToken);

        if (booking is null)
        {
            return DomainError.NotFound(BookingErrors.NotFound);
        }

        var result = booking.Complete(_dateTimeProvider.UTCNow);

        if (result.IsError)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}