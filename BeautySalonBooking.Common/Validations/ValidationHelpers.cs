namespace BeautySalonBooking.Common.Validations;

public static class ValidationHelpers
{
    public static string NormalizeDigits(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        const string persianDigits = "۰۱۲۳۴۵۶۷۸۹";
        const string arabicDigits = "٠١٢٣٤٥٦٧٨٩";

        return string.Concat(
            value.Select(c =>
            {
                var persianIndex = persianDigits.IndexOf(c);

                if (persianIndex >= 0)
                    return persianIndex.ToString()[0];

                var arabicIndex = arabicDigits.IndexOf(c);

                if (arabicIndex >= 0)
                    return arabicIndex.ToString()[0];

                return c;
            }));
    }

    public static bool IsValidIranianMobile(string? value)
    {
        var mobile = NormalizeDigits(value)
            .Trim()
            .Replace(" ", "")
            .Replace("-", "");

        var validPrefixes = new HashSet<string>
        {
            // MCI
            "910", "911", "912", "913", "914", "915", "916", "917", "918", "919",
            "990", "991", "992", "993", "994",

            // Irancell
            "901", "902", "903", "904", "905",
            "930", "933", "935", "936", "937", "938", "939",

            // Rightel
            "920", "921", "922"
        };

        return mobile.Length == 10 &&
               mobile.StartsWith("9") &&
               mobile.All(char.IsDigit) &&
               validPrefixes.Contains(mobile[..3]);
    }

    public static bool IsValidIranianNationalCode(string? value)
    {
        var code = NormalizeDigits(value).Trim();

        if (code.Length != 10)
            return false;

        if (!code.All(char.IsDigit))
            return false;

        if (code.Distinct().Count() == 1)
            return false;

        var checkDigit = code[9] - '0';

        var sum = 0;

        for (var i = 0; i < 9; i++)
        {
            sum += (code[i] - '0') * (10 - i);
        }

        var remainder = sum % 11;

        var calculatedCheckDigit =
            remainder < 2
                ? remainder
                : 11 - remainder;

        return checkDigit == calculatedCheckDigit;
    }

    public static bool IsValidIranianPostalCode(string? value)
    {
        var code = NormalizeDigits(value).Trim();

        if (code.Length != 10)
            return false;

        if (!code.All(char.IsDigit))
            return false;

        if (code.Distinct().Count() == 1)
            return false;

        if (code.StartsWith("0") || code.StartsWith("2"))
            return false;

        return true;
    }

    public static bool IsValidEmail(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return System.Text.RegularExpressions.Regex.IsMatch(
            value.Trim(),
            @"^[^\s@]+@[^\s@]+\.[^\s@]{2,}$");
    }

    public static bool IsValidOtp(string? value, int length = 6)
    {
        var otp = NormalizeDigits(value);

        return otp.Length == length &&
               otp.All(char.IsDigit);
    }
}