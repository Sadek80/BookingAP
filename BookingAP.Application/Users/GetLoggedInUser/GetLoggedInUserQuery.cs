using BookingAp.Contract.Users;
using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace BookingAP.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery() : IQuery<ErrorOr<UserResponse>>;