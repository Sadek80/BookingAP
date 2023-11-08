using BookingAP.Domain.Users;
using BookingAP.Domain.Users.Repositories;
using BookingAP.Domain.Users.ValueObjects;

namespace BookingAP.Infrastructure.Repositories.Users
{
    internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
