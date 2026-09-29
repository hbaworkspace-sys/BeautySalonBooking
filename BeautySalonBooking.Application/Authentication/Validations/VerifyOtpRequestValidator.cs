using BeautySalonBooking.Common.Validations;
using BeautySalonBooking.Contracts.Authentication.Requests;
using FluentValidation;

namespace BeautySalonBooking.Application.Authentication.Validations;

public sealed class VerifyOtpRequestValidator
    : AbstractValidator<VerifyOtpRequest>
{
    public VerifyOtpRequestValidator()
    {
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage("شماره موبایل الزامی است.")
            .Must(ValidationHelpers.IsValidIranianMobile)
            .When(x => !string.IsNullOrWhiteSpace(x.MobileNumber))
            .WithMessage("شماره موبایل معتبر نیست.");

        RuleFor(x => x.OtpCode)
            .NotEmpty()
            .WithMessage("کد تایید الزامی است.")
            .Must(x => ValidationHelpers.IsValidOtp(x))
            .When(x => !string.IsNullOrWhiteSpace(x.OtpCode))
            .WithMessage("کد تایید باید ۶ رقمی باشد.");
    }
}