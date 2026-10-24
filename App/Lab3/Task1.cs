using System.Text.RegularExpressions;

namespace App.Lab3;

public class User
{
    private readonly int id;
    private string login;
    private string password;
    private string name;
    private string surname;
    private int inn;
    private string phone;
    private readonly DateTime registerDate;
    
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
    }
    
    private static readonly Regex Pattern = new Regex(
        @"^(?:\+7|8)[-\s]?\(?\d{3}\)?[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$",
        RegexOptions.Compiled
    );

    string GetUserFullName()
    {
        return name + " " + surname;
    }

    public bool TryUpdatePhone(string phone)
    {
        if (!IsPhoneValid(phone))
            return false;
        this.phone = phone;
        return true;
    }
    
    private static bool IsPhoneValid(string phone)
    {
        return Pattern.IsMatch(phone);
    }
    
    public override string ToString()
    {
        return $"ID: {id}, Login: {login}, Password {password}, Name: {GetUserFullName()}, Phone: {phone}, Inn: {inn}, RegisterDate: {registerDate}, ";
    }
}

public class Task1
{
    public static void Main()
    {
        var user1 = new User("first", "111", "Max", "Tokarev", 123456, "89130125163");
        if (user1.TryUpdatePhone("+7 (913) 012-51-63"))
            Console.WriteLine("Phone updated successfully");

        var user2 = new User("second", "222", "Kompot", "Vishnevii", 888888, "88888888888");
        Console.WriteLine("User1:\n" + user1);
        Console.WriteLine("\nUser2:\n" + user2);
    }
}