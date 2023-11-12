using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Users.Abstractions;
using BookingAP.Domain.Users.ValueObjects;

namespace BookingAP.Domain.Users.Repositories
{
    public interface IUserRepository : IRepository<User, UserId>
    {
        Task<TResult?> GetUserByIdentityIdAsync<TResult>(string IdentityId, CancellationToken cancellationToken = default) where TResult : IUserResponse;
    }
}
