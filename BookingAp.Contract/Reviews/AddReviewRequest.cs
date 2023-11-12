namespace BookingAp.Contract.Reviews;

public sealed record AddReviewRequest(Guid BookingId, int Rating, string Comment);