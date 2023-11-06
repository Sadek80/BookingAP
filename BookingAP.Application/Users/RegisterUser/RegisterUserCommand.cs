using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace BookingAP.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
        string Email,
        string FirstName,
        string LastName,
        string Password) : ICommand<ErrorOr<Guid>>;