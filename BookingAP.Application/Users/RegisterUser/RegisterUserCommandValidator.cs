using FluentValidation;

namespace BookingAP.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();

        RuleFor(c => c.LastName).NotEmpty();

        RuleFor(c => c.Email).EmailAddress();

        RuleFor(c => c.Password).NotEmpty()
                                .MinimumLength(8)
                                .Must(HasLoweCase)
                                .Must(HasUpperCase)
                                .Must(HasDigit)
                                .Must(HasDigit)
                                .Must(HasSpecialChar)
                                .WithMessage("Password Should be at least 8 characters containing Lower and Upper case with Special Character.");
    }

    private bool HasLoweCase(string password)
    {
        return password.Any(c => c >= 'a' && c <= 'z');
    }

    private bool HasUpperCase(string password)
    {
        return password.Any(c => c >= 'A' && c <= 'Z');
    }

    private bool HasDigit(string password)
    {
        return password.Any(c => c >= '0' && c <= '9');
    }

    private bool HasSpecialChar(string password)
    {
        return !HasDigit(password) && !HasLoweCase(password) && !HasUpperCase(password);
    }
}