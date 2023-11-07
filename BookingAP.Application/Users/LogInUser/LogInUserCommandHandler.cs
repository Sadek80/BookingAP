using BookingAp.Contract.Users;
using BookingAP.Application.Abstractions.Authentication;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Abstractions;
using ErrorOr;
using static BookingAP.Domain.Users.DomainErrors;

namespace BookingAP.Application.Users.LogInUser;

internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, ErrorOr<AccessTokenResponse>>
{
    private readonly IJwtService _jwtService;

    public LogInUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<ErrorOr<AccessTokenResponse>> Handle(
        LogInUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsError)
        {
            return DomainError.Failure(UserErrors.InvalidCredentials);
        }

        return new AccessTokenResponse(result.Value);
    }
}