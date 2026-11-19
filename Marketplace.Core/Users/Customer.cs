namespace Marketplace.Core.Users;

public class Customer : User
{
    private readonly List<Order> orders = new();

    public Cart Cart { get; } = new();

    public Customer()
    {
    }

    public Customer(string login, string passwordHash, string name, string surname, string inn, string phone)
        : base(login, passwordHash, name, surname, inn, phone)
    {
    }

    public void AddOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        orders.Add(order);
    }

    public Order[] GetOrders() => orders.ToArray();
}