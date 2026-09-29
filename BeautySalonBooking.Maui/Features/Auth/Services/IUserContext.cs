using BeautySalonBooking.Contracts.Authentication.Dtos;

namespace BeautySalonBooking.Maui.Features.Auth.Services;

public interface IUserContext
{
    long UserId { get; }

    string FirstName { get; }
    string LastName { get; }
    string FullName { get; }

    string Email { get; }
    string PhoneNumber { get; }
    string NationalCode { get; }
    string UserName { get; }

    bool IsActive { get; }
    int AuthenticationType { get; }

    IReadOnlyList<string> Roles { get; }
    IReadOnlyList<PermissionDto> Permissions { get; }
    IReadOnlyList<PermissionDto> Menus { get; }

    bool IsAuthenticated { get; }

    void SetUser(UserDto user);

    void Clear();
}