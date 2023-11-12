using BookingAP.Domain.Users.Abstractions;

namespace BookingAp.Contract.Users
{
    public sealed class UserIdDto : IUserResponse
    {
        public Guid Id { get; init; }
    }
}
