using System;
using System.Security.Cryptography;
using System.Text;

namespace App.Lab3;

public class User
{
    private static int counter = 0;

    // Свойства с модификатором init можно задать только в конструкторе или инициализаторе
    public int Id { get; init; }
    public DateTime RegisterDate { get; init; }
    
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Inn { get; set; }

    private string _phone;
    public string Phone
    {
        get => _phone;
        set
        {
            if (TryUpdatePhone(value, out var normalized))
                _phone = normalized;
            else
                throw new ArgumentException($"Неверный формат номера телефона: {value}");
        }
    }

    public User()
    {
        Id = counter++;
        RegisterDate = DateTime.Now;
    }

    public User(string login, string passwordHash, string name, string surname, int inn, string phone) : this()
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Inn = inn;
        Phone = phone;
    }

    public string GetUserFullName() => $"{Name} {Surname}";

    private static bool TryUpdatePhone(string input, out string normalized)
    {
        normalized = string.Empty;
        if (string.IsNullOrWhiteSpace(input)) return false;

        var sb = new StringBuilder(16);
        sb.Append('+');

        var isFirstDigit = true;
        var hasExplicitPlus = false;

        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (i == 0 && c == '+')
            {
                hasExplicitPlus = true;
                continue;
            }

            if (char.IsDigit(c))
            {
                if (isFirstDigit)
                {
                    if (!hasExplicitPlus && c == '8') sb.Append('7');
                    else sb.Append(c);
                    isFirstDigit = false;
                }
                else
                    sb.Append(c);
            }
            else if (c != ' ' && c != '-' && c != '(' && c != ')')
            {
                return false;
            }
        }

        var digitsCount = sb.Length - 1;
        if (digitsCount is < 10 or > 15) return false;
        normalized = sb.ToString();
        return true;

    }

    public override string ToString()
    {
        return $"ID: {Id}, Login: {Login}, PasswordHash: {PasswordHash}, Name: {GetUserFullName()}, Phone: {Phone}, Inn: {Inn}, RegDate: {RegisterDate}";
    }
}

public static class UserCreator
{
    public static User CreateUser(string login, string password, string name, string surname, int inn, string phone)
    {
        var passwordHash = CalculateMd5Hash(password);
        return new User(login, passwordHash, name, surname, inn, phone);
    }

    private static string CalculateMd5Hash(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = MD5.HashData(inputBytes);
        
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}