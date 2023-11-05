using BookingAP.Domain.Appartments;
using BookingAP.Domain.Appartments.Repositories;

namespace BookingAP.Infrastructure.Repositories.Appartments
{
    internal sealed class AppartmentRepository : Repository<Appartment>, IAppartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
