using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments.ValueObjects;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Bookings.Enums;
using BookingAP.Domain.Bookings.ValueObjects;
using BookingAP.Domain.Reviews.Events;
using BookingAP.Domain.Reviews.ValueObjects;
using BookingAP.Domain.Users.ValueObjects;
using ErrorOr;

namespace BookingAP.Domain.Reviews
{
    public sealed class Review : Entity<ReviewId>
    {
        private Review(ReviewId Id,
                      UserId userId,
                      AppartmentId appartmentId,
                      BookingId bookingId,
                      Comment comment,
                      Rating rating,
                      DateTime createdOnUTC)
            :base(Id)
        {
            UserId = userId;
            AppartmentId = appartmentId;
            BookingId = bookingId;
            Comment = comment;
            Rating = rating;
            CreatedOnUTC = createdOnUTC;
        }

        private Review()
        {
        }
        public UserId UserId { get; private set; }
        public AppartmentId AppartmentId { get; private set; }
        public BookingId BookingId { get; private set; }
        public Comment Comment { get; private set; }
        public Rating Rating { get; internal set; }
        public DateTime CreatedOnUTC { get; private set; }

        public static ErrorOr<Review> Create(Booking booking,
                                             Rating rating,
                                             Comment comment,
                                             DateTime createdOnUtc)
        {
            if(booking.Status != BookingStatus.Completed)
            {
                return DomainError.Failure(DomainErrors.ReviewErrors.NotEligible);
            }

            var review = new Review(ReviewId.New,
                                    booking.UserId,
                                    booking.AppartmentId,
                                    booking.Id,
                                    comment,
                                    rating,
                                    createdOnUtc);

            review.RaisDomainEvent(new ReviewCreatedDomainEvent(review.Id));

            return review;
        }
    }
}
