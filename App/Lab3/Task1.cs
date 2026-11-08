using System.Text;
using System.Text.RegularExpressions;

namespace App.Lab3;

public class User
{
    private readonly int id;
    private readonly DateTime registerDate;
    private string login;
    private string password;
    private string name;
    private string surname;
    private int inn;
    private string phone;
    
    private static int counter = 0;
    public User(string login, string password, string name, string surname, int inn, string phone)
    {
        this.login = login;
        this.password = password;
        this.name = name;
        this.surname = surname;
        this.inn = inn;
        registerDate = DateTime.Now;
        id = counter++;
        
        this.phone = TryNormalizePhone(phone, out string normalizedPhone) ? normalizedPhone : "Invalid";
    }
    
    public string GetUserFullName() => name + " " + surname;
    
    public string Phone => phone;

    public bool TryUpdatePhone(string rawPhone)
    {
        if (!TryNormalizePhone(rawPhone, out var normalizedPhone))
            return false;
        
        this.phone = normalizedPhone;
        return true;
    }
    
    private static bool TryNormalizePhone(string input, out string normalized)
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
                    if (!hasExplicitPlus && c == '8')
                        sb.Append('7');
                    else
                        sb.Append(c);
                    isFirstDigit = false;
                }
                else
                    sb.Append(c);
            }
            else if (c != ' ' && c != '-' && c != '(' && c != ')')
                return false;
        }

        var digitsCount = sb.Length - 1;
        if (digitsCount is < 10 or > 15) return false;
        normalized = sb.ToString();
        return true;

    }
    
    public override string ToString()
    {
        return $"ID: {id}, Login: {login}, Password {password}, Name: {GetUserFullName()}, Phone: {phone}, Inn: {inn}, RegisterDate: {registerDate}, ";
    }
}