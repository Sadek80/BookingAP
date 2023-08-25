using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Reviews
{
    public static partial class DomainErrors
    {
        public static class ReviewErrors
        {
            public static DomainError NotEligible = new(
                   "Review.NotEligible",
                   "The review is not eligible because the booking is not yet completed");
        }
    }
}
