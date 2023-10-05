using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace BookingAP.Application.Reviews.AddReview;

public sealed record AddReviewCommand(Guid BookingId, int Rating, string Comment) : ICommand<ErrorOr<Guid>>;