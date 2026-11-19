namespace Marketplace.Core.Users;

public class Seller : User
{
    private readonly List<Product> products = new();
    private readonly List<Order> orders = new();

    public string CompanyInn { get; set; }

    public Seller()
    {
    }

    public Seller(string login, string passwordHash, string name, string surname, string inn, string phone, string companyInn)
        : base(login, passwordHash, name, surname, inn, phone)
    {
        CompanyInn = companyInn;
    }

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        products.Add(product);
    }

    public Product[] GetProducts() => products.ToArray();

    public void AddOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        orders.Add(order);
    }

    public Order[] GetOrders() => orders.ToArray();
}