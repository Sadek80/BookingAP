using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Reviews.ValueObjects
{
    public static partial class DomainErrors
    {
        public static class RatingErrors
        {
            public static DomainError Invalid = new(
                    "Rating.Invalid",
                    "The rating is invalid");
        }
    }
}
