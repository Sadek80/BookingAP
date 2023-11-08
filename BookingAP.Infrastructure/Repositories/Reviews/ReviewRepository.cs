using BookingAP.Domain.Reviews;
using BookingAP.Domain.Reviews.Repositories;
using BookingAP.Domain.Reviews.ValueObjects;

namespace BookingAP.Infrastructure.Repositories.Reviews
{
    internal sealed class ReviewRepository : Repository<Review, ReviewId>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
