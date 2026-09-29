using BeautySalonBooking.Contracts.Authentication.Requests;
using FluentValidation;

namespace BeautySalonBooking.Application.Authentication.Validations;

public sealed class RefreshTokenRequestValidator
    : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .WithMessage("Access Token الزامی است.");

        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh Token الزامی است.");
    }
}