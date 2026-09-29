using BeautySalonBooking.Common.Validations;
using BeautySalonBooking.Contracts.Authentication.Requests;
using FluentValidation;

namespace BeautySalonBooking.Application.Authentication.Validations;

public sealed class LoginInitiateRequestValidator
    : AbstractValidator<LoginInitiateRequest>
{
    public LoginInitiateRequestValidator()
    {
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage("شماره موبایل الزامی است.");

        RuleFor(x => x.MobileNumber)
            .Must(ValidationHelpers.IsValidIranianMobile)
            .When(x => !string.IsNullOrWhiteSpace(x.MobileNumber))
            .WithMessage("شماره موبایل معتبر نیست.");
    }
}