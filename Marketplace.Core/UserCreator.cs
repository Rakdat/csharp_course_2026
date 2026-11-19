using System.Security.Cryptography;
using System.Text;

namespace Marketplace.Core;

public static class UserCreator
{
    public static User CreateUser(string login, string password, string name, string surname, string inn, string phone)
    {
        var passwordHash = CalculateMd5Hash(password);
        return new User(login, passwordHash, name, surname, inn, phone);
    }

    private static string CalculateMd5Hash(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = MD5.HashData(inputBytes);
        
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}