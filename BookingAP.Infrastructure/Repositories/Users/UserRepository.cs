using BookingAP.Domain.Users;
using BookingAP.Domain.Users.Repositories;

namespace BookingAP.Infrastructure.Repositories.Users
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
