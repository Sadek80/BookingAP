using BookingAP.Application.Abstractions.Authentication;
using BookingAP.Application.Abstractions.Data;
using BookingAP.Application.Abstractions.Messaging;
using Dapper;
using ErrorOr;

namespace BookingAP.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler
    : IQueryHandler<GetLoggedInUserQuery, ErrorOr<UserResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;

    public GetLoggedInUserQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<ErrorOr<UserResponse>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                "id" AS Id,
                "name" AS Name,
                "email" AS Email
            FROM "users"
            WHERE "IdentityId" = @IdentityId
            """;

        var user = await connection.QuerySingleAsync<UserResponse>(
            sql,
            new
            {
                _userContext.IdentityId
            });

        return user;
    }
}