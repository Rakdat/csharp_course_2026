namespace Marketplace.Core;

public class Product
{
    private static int counter = 1;

    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public ProductCategory Categories { get; }

    public Product(string name, decimal price, ProductCategory categories)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название товара не может быть пустым.", nameof(name));
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Цена не может быть отрицательной.");

        Id = counter++;
        Name = name;
        Price = price;
        Categories = categories;
    }
}