namespace Marketplace.Core.Users;

public class Admin : User
{
    private readonly List<Employee> employees = new();

    public Admin()
    {
    }

    public Admin(string login, string passwordHash, string name, string surname, string inn, string phone)
        : base(login, passwordHash, name, surname, inn, phone)
    {
    }

    public void AddEmployee(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);
        employees.Add(employee);
    }

    public Employee[] GetEmployees() => employees.ToArray();
}