using BookingAP.Application.Abstractions.Clock;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Bookings.ValueObjects;
using ErrorOr;
using static BookingAP.Domain.Bookings.DomainErrors;

namespace Bookify.Application.Bookings.RejectBooking;

internal sealed class RejectBookingCommandCommandHandler : ICommandHandler<RejectBookingCommand, ErrorOr<bool>>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RejectBookingCommandCommandHandler(
        IDateTimeProvider dateTimeProvider,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<bool>> Handle(RejectBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(new BookingId(request.BookingId), cancellationToken);

        if (booking is null)
        {
            return DomainError.NotFound(BookingErrors.NotFound);
        }

        var result = booking.Reject(_dateTimeProvider.UTCNow);

        if (result.IsError)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}