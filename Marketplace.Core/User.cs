using System.Text;

namespace Marketplace.Core;

public class User
{
    private static int counter = 0;

    public Guid Id { get; init; }
    public DateTime RegisterDate { get; init; }
    public string Login { get; init; }
    
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Inn { get; set; }

    private string phone;
    public string Phone
    {
        get => phone;
        set
        {
            if (!TryUpdatePhone(value))
                throw new ArgumentException($"Неверный формат номера телефона: {value}");
        }
    }

    public User()
    {
        Id = Guid.NewGuid();
        RegisterDate = DateTime.Now;
    }

    public User(string login, string passwordHash, string name, string surname, string inn, string phone) : this()
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Inn = inn;
        Phone = phone;
    }

    public string GetUserFullName() => $"{Name} {Surname}";

    private bool TryUpdatePhone(string newPhone)
    {
        if (!IsPhoneValid(newPhone, out var normalized))
            return false;
        phone = normalized;
        return true;
    }

    private static bool IsPhoneValid(string input, out string normalized)
    {
        normalized = string.Empty;
        if (string.IsNullOrWhiteSpace(input))
            return false;

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
                    if (!hasExplicitPlus && c == '8')
                        sb.Append('7');
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
        if (digitsCount is < 10 or > 15)
            return false;
        normalized = sb.ToString();
        return true;
    }

    public override string ToString() => $"ID: {Id}, Login: {Login}, PasswordHash: {PasswordHash}, Name: {GetUserFullName()}, Phone: {Phone}, Inn: {Inn}, RegDate: {RegisterDate}";
}