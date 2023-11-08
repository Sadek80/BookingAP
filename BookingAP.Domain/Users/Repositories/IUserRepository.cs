using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Users.ValueObjects;

namespace BookingAP.Domain.Users.Repositories
{
    public interface IUserRepository : IRepository<User, UserId>
    {
    }
}
