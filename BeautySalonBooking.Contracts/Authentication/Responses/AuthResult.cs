using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Permission.Dtos;

namespace BeautySalonBooking.Contracts.Authentication.Responses;

// در کلاس AuthResult
public class AuthResult
{
    public AuthResult() { }

    public bool IsSuccess { get; set; }  // ✅ اضافه شد
    public string Error { get; set; } = string.Empty;  // ✅ اضافه شد
    public TokenResponse? Tokens { get; set; }  // ✅ اضافه شد
    public UserDto? User { get; set; }  // ✅ اضافه شد
    //public UserMenuResponse? UserMenus { get; set; }  // ✅ اضافه شد

    public AuthResult(TokenResponse tokens, UserDto user)
    {
        IsSuccess = true;
        Error = string.Empty;
        Tokens = tokens;
        User = user;
        // UserMenus = userMenus;
    }

    private AuthResult(bool success, string error, TokenResponse? tokens, UserDto? user)
    {
        IsSuccess = success;
        Error = error;
        Tokens = tokens;
        User = user;
        //UserMenus = userMenus;
    }

    public static AuthResult Success(TokenResponse tokens, UserDto user)
        => new(true, string.Empty, tokens, user);

    public static AuthResult Failure(string error)
        => new(false, error, null, null);
}