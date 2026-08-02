namespace BeautySalonBooking.Contracts.Authentication.Dtos;

public class UserDto
{
    public long Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string NationalCode { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public int AuthenticationType { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string? FullName => $"{FirstName} {LastName}";

    public List<string> Roles { get; set; } = new();

    public List<PermissionDto> Permissions { get; set; } = new();

    public List<PermissionDto> Menus { get; set; } = new();
}

public class CreateUserRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int AuthenticationType { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class UpdateUserRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int AuthenticationType { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;

}

