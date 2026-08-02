using System.Security.Cryptography;
using System.Text;

namespace BeautySalonBooking.Application.Authentication.Helpers;

public static class HashHelper
{
    public static string ComputeSha256(string value)
    {
        using var sha = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(value);

        var hash = sha.ComputeHash(bytes);

        return Convert.ToHexString(hash);
    }

    public static bool Verify(string value, string hash)
    {
        return ComputeSha256(value)
            .Equals(hash, StringComparison.OrdinalIgnoreCase);
    }
}