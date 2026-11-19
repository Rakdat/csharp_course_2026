namespace Marketplace.Core.Users;

public class Employee : User
{
    private readonly List<Order> orders = new();

    public string CompanyInn { get; set; }

    public Employee()
    {
    }

    public Employee(string login, string passwordHash, string name, string surname, string inn, string phone, string companyInn)
        : base(login, passwordHash, name, surname, inn, phone)
    {
        CompanyInn = companyInn;
    }

    public void AddOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        orders.Add(order);
    }

    public Order[] GetOrders() => orders.ToArray();
}