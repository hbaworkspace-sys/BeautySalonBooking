using BeautySalonBooking.Common.Validations;
using BeautySalonBooking.Contracts.Authentication.Enums;

namespace BeautySalonBooking.Maui.Features.Auth.Validators;

public sealed class AuthValidator
{
    public string? ValidateRegister(
        string? firstName,
        string? lastName,
        string? mobileNumber,
        string? nationalCode,
        Gender? gender)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return "نام را وارد کنید.";

        if (firstName.Length > 50)
            return "نام نباید بیشتر از 50 کاراکتر باشد.";

        if (string.IsNullOrWhiteSpace(lastName))
            return "نام خانوادگی را وارد کنید.";

        if (lastName.Length > 50)
            return "نام خانوادگی نباید بیشتر از 50 کاراکتر باشد.";

        if (string.IsNullOrWhiteSpace(mobileNumber))
            return "شماره موبایل را وارد کنید.";

        if (!ValidationHelpers.IsValidIranianMobile(mobileNumber))
            return "شماره موبایل معتبر نیست.";

        if (string.IsNullOrWhiteSpace(nationalCode))
            return "کد ملی را وارد کنید.";

        if (!ValidationHelpers.IsValidIranianNationalCode(nationalCode))
            return "کد ملی معتبر نیست.";

        if (gender is null)
            return "لطفاً جنسیت خود را انتخاب کنید.";

        return null;
    }

    public string? ValidateLogin(string? mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
            return "شماره موبایل را وارد کنید.";

        if (!ValidationHelpers.IsValidIranianMobile(mobileNumber))
            return "شماره موبایل معتبر نیست.";

        return null;
    }

    public string? ValidateOtp(string? otpCode)
    {
        if (string.IsNullOrWhiteSpace(otpCode))
            return "کد تأیید را وارد کنید.";

        if (!ValidationHelpers.IsValidOtp(otpCode))
            return "کد تأیید باید ۶ رقم باشد.";

        return null;
    }
}