using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Users
{
    public static partial class DomainErrors
    {
        public static class UserErrors
        {
            public static DomainError NotFound = new(
               "User.NotFound",
               "The user with the specified identifier was not found");

            public static DomainError InvalidCredentials = new(
                "User.InvalidCredentials",
                "The provided credentials were invalid");
        }
    }
}
