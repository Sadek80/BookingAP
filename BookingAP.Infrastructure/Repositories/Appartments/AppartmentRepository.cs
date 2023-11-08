using BookingAP.Domain.Appartments;
using BookingAP.Domain.Appartments.Repositories;
using BookingAP.Domain.Appartments.ValueObjects;

namespace BookingAP.Infrastructure.Repositories.Appartments
{
    internal sealed class AppartmentRepository : Repository<Appartment, AppartmentId>, IAppartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
