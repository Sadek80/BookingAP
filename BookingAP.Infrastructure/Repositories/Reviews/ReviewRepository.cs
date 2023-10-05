using BookingAP.Domain.Reviews;
using BookingAP.Domain.Reviews.Repositories;

namespace BookingAP.Infrastructure.Repositories.Reviews
{
    internal sealed class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
