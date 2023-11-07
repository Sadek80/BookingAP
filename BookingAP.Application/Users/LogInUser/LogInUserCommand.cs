using BookingAp.Contract.Users;
using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace BookingAP.Application.Users.LogInUser;

public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<ErrorOr<AccessTokenResponse>>;