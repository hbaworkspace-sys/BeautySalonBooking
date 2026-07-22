using BeautySalonBooking.Domain.Base.Enums;

namespace BeautySalonBooking.Domain.ContactAggregate.Entities;

public class PhoneNumber
{
    public string Number { get; private set; } = null!;
    public PhoneNumberType Type { get; private set; }
    public bool IsDefault { get; private set; }
    public bool IsVerified { get; private set; }

    private PhoneNumber()
    {
    }

    public static PhoneNumber Create(
        string number,
        PhoneNumberType type,
        bool isDefault = false)
    {
        ValidateNumber(number);

        return new PhoneNumber
        {
            Number = Normalize(number),
            Type = type,
            IsDefault = isDefault,
            IsVerified = false
        };
    }

    public void ChangeNumber(string number)
    {
        ValidateNumber(number);

        Number = Normalize(number);
        IsVerified = false;
    }

    public void ChangeType(PhoneNumberType type)
    {
        Type = type;
    }

    public void SetAsDefault()
    {
        IsDefault = true;
    }

    public void RemoveAsDefault()
    {
        IsDefault = false;
    }

    public void Verify()
    {
        IsVerified = true;
    }

    public void Unverify()
    {
        IsVerified = false;
    }

    private static void ValidateNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number cannot be empty.", nameof(number));

        number = Normalize(number);

        if (number.Length != 10)
            throw new ArgumentException("Phone number must be 10 digits.", nameof(number));

        if (!number.All(char.IsDigit))
            throw new ArgumentException("Phone number must contain only digits.", nameof(number));
    }

    private static string Normalize(string number)
    {
        return number.Trim();
    }
}