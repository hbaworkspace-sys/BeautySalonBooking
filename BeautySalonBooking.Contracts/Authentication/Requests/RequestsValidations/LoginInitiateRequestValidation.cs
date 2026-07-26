using FluentValidation;

namespace BeautySalonBooking.Contracts.Authentication.Requests.RequestsValidations;
public class LoginInitiateRequestValidation : AbstractValidator<LoginInitiateRequest>
{
    public LoginInitiateRequestValidation()
    {
        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("شماره موبایل الزامی است.")
            .Length(10).WithMessage("شماره موبایل باید ۱۰ رقم باشد.")
            .Matches(@"^\d{10}$").WithMessage("شماره موبایل فقط باید شامل ارقام باشد.");

    }
}
