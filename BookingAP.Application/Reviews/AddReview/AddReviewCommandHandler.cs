using BookingAP.Application.Abstractions.Clock;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Reviews.ValueObjects;
using BookingAP.Domain.Reviews;
using ErrorOr;
using static BookingAP.Domain.Bookings.DomainErrors;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Reviews.Repositories;
using BookingAP.Application.Reviews.AddReview;

namespace Bookify.Application.Reviews.AddReview;

internal sealed class AddReviewCommandHandler : ICommandHandler<AddReviewCommand, ErrorOr<Guid>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddReviewCommandHandler(
        IBookingRepository bookingRepository,
        IReviewRepository reviewRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _bookingRepository = bookingRepository;
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Guid>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);

        if (booking is null)
        {
            return DomainError.NotFound(BookingErrors.NotFound);
        }

        var ratingResult = Rating.Create(request.Rating);

        if (ratingResult.IsError)
        {
            return ratingResult.FirstError;
        }

        var reviewResult = Review.Create(booking,
                                         ratingResult.Value,
                                         new Comment(request.Comment),
                                         _dateTimeProvider.UTCNow);

        if (reviewResult.IsError)
        {
            return reviewResult.FirstError;
        }

        _reviewRepository.Add(reviewResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return reviewResult.Value.Id;
    }
}