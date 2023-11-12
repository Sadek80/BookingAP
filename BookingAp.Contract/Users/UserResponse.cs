using BookingAP.Domain.Users.Abstractions;

namespace BookingAp.Contract.Users;

public sealed class UserResponse : IUserResponse
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}