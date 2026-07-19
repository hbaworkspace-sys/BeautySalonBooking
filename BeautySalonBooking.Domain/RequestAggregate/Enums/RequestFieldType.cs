namespace BeautySalonBooking.Domain.RequestAggregate.Enums;

public enum RequestFieldType : byte
{
    Text = 1,
    Number = 2,
    Decimal = 3,
    Boolean = 4,
    Date = 5,
    DateTime = 6,
    Mobile = 7,
    Email = 8,
    NationalCode = 9,
    Select = 10,
    MultiSelect = 11,
    TextArea = 12
}