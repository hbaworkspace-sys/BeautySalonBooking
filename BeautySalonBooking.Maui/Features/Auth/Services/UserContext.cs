using BeautySalonBooking.Contracts.Authentication.Dtos;

namespace BeautySalonBooking.Maui.Features.Auth.Services;

public sealed class UserContext : IUserContext
{
    public long UserId { get; private set; }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string NationalCode { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }
    public int AuthenticationType { get; private set; }

    public IReadOnlyList<string> Roles { get; private set; } = [];
    public IReadOnlyList<PermissionDto> Permissions { get; private set; } = [];
    public IReadOnlyList<PermissionDto> Menus { get; private set; } = [];

    public bool IsAuthenticated => UserId > 0;

    public void SetUser(UserDto user)
    {
        UserId = user.Id;

        FirstName = user.FirstName;
        LastName = user.LastName;
        FullName = user.FullName ?? string.Empty;
        Email = user.Email;
        PhoneNumber = user.PhoneNumber;
        NationalCode = user.NationalCode;
        UserName = user.UserName;

        IsActive = user.IsActive;
        AuthenticationType = user.AuthenticationType;

        Roles = user.Roles?.ToList() ?? [];
        Permissions = user.Permissions?.ToList() ?? [];
        Menus = user.Menus?.ToList() ?? [];
    }

    public void Clear()
    {
        UserId = 0;

        FirstName = string.Empty;
        LastName = string.Empty;
        FullName = string.Empty;

        Email = string.Empty;
        PhoneNumber = string.Empty;
        NationalCode = string.Empty;
        UserName = string.Empty;

        IsActive = false;
        AuthenticationType = 0;

        Roles = [];
        Permissions = [];
        Menus = [];
    }
}