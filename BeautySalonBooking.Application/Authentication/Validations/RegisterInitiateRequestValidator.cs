using BeautySalonBooking.Common.Validations;
using BeautySalonBooking.Contracts.Authentication.Enums;
using BeautySalonBooking.Contracts.Authentication.Requests;
using FluentValidation;

namespace BeautySalonBooking.Application.Authentication.Validations;

public sealed class RegisterInitiateRequestValidator
    : AbstractValidator<RegisterInitiateRequest>
{
    public RegisterInitiateRequestValidator()
    {
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage("شماره موبایل الزامی است.")
            .Must(ValidationHelpers.IsValidIranianMobile)
            .When(x => !string.IsNullOrWhiteSpace(x.MobileNumber))
            .WithMessage("شماره موبایل معتبر نیست.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("نام الزامی است.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("نام خانوادگی الزامی است.");

        RuleFor(x => x.NationalCode)
            .NotEmpty()
            .WithMessage("کد ملی الزامی است.")
            .Must(ValidationHelpers.IsValidIranianNationalCode)
            .When(x => !string.IsNullOrWhiteSpace(x.NationalCode))
            .WithMessage("کد ملی معتبر نیست.");

        RuleFor(x => x.Gender)
            .NotNull()
            .WithMessage("لطفاً جنسیت را انتخاب کنید.")
            .Must(gender =>
                gender == Gender.Male ||
                gender == Gender.Female)
            .WithMessage("جنسیت انتخاب شده معتبر نیست.");
    }
}