using BookingAp.Contract.Users;
using BookingAP.Application.Abstractions.Authentication;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Users;
using BookingAP.Domain.Users.Repositories;
using BookingAP.Domain.Users.ValueObjects;
using ErrorOr;

namespace BookingAP.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ErrorOr<RegisterUserResponse>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IAuthenticationService authenticationService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RegisterUserResponse>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email));

        var identityId = await _authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync();

        return new RegisterUserResponse(user.Id);
    }
}