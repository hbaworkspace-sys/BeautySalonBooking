using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.GeographyAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Eunms;

namespace BeautySalonBooking.Domain.PersonAggregate.Entities;

public class Person : AuditableSoftDeleteEntity<long>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? FatherName { get; private set; }
    public string? NationalCode { get; private set; }
    public Gender Gender { get; private set; } = Gender.Unknown;
    public DateOnly? BirthDate { get; private set; }
    public int? BirthRegionId { get; private set; }
    public Region? BirthRegion { get; private set; }
    public string? BirthCertificateNumber { get; private set; }
    public string? BirthCertificateSerial { get; private set; }

    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    private Person()
    {
    }

    public static Person Create(
        string firstName,
        string lastName)
    {
        ValidateName(firstName, lastName);

        return new Person
        {
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Gender = Gender.Unknown
        };
    }

    public void ChangeName(string firstName, string lastName)
    {
        ValidateName(firstName, lastName);

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    public void ChangeGender(Gender gender)
    {
        Gender = gender;
    }

    public void UpdateIdentity(string? fatherName, string? nationalCode, DateOnly? birthDate, int? birthRegionId,
        string? birthCertificateNumber, string? birthCertificateSerial)
    {
        FatherName = Normalize(fatherName);
        NationalCode = Normalize(nationalCode);

        BirthDate = birthDate;
        BirthRegionId = birthRegionId;

        BirthCertificateNumber = Normalize(birthCertificateNumber);
        BirthCertificateSerial = Normalize(birthCertificateSerial);
    }

    public void ChangeBirthDate(DateOnly? birthDate)
    {
        BirthDate = birthDate;
    }

    public void ChangeBirthRegion(int? birthRegionId)
    {
        BirthRegionId = birthRegionId;
    }

    public void AddUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (_users.Any(x => x.Id == user.Id))
            return;

        _users.Add(user);
    }

    public void RemoveUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var existing = _users.FirstOrDefault(x => x.Id == user.Id);

        if (existing is null)
            return;

        _users.Remove(existing);
    }

    private static void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
    }

    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}