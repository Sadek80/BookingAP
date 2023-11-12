using BookingAp.Contract.Users;
using BookingAP.Application.Abstractions.Authentication;
using BookingAP.Application.Abstractions.Data;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments;
using BookingAP.Domain.Users.Repositories;
using Dapper;
using ErrorOr;
using static BookingAP.Domain.Users.DomainErrors;

namespace BookingAP.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler
    : IQueryHandler<GetLoggedInUserQuery, ErrorOr<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;

    public GetLoggedInUserQueryHandler(
        IUserRepository userRepository,
        IUserContext userContext)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }

    public async Task<ErrorOr<UserResponse>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdentityIdAsync<UserResponse>(_userContext.IdentityId, cancellationToken);

        if(user is null)
        {
            return DomainError.NotFound(UserErrors.NotFound);
        }

        return user;
    }
}