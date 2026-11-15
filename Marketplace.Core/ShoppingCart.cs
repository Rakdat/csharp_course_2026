namespace Marketplace.Core;

public class ShoppingCart
{
    private readonly List<Product> products = new();
    private decimal secretDiscount;              

    public User Owner { get; }

    public ShoppingCart(User owner)
    {
        Owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }

    public int Count => products.Count;

    public Product this[int index]
    {
        get
        {
            if (index < 0 || index >= products.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс находится вне границ корзины.");
            
            return products[index];
        }
    }

    public decimal TotalPrice
    {
        get
        {
            var sum = 0m;
            foreach (var product in products)
                sum += CalculateDiscountedProductPrice(product);

            var finalTotal = sum - secretDiscount;
            return finalTotal > 0m ? Math.Round(finalTotal, 2) : 0m;
        }
    }

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        products.Add(product);
    }

    public void Clear()
    {
        products.Clear();
        secretDiscount = 0m;
    }

    internal void SetDiscount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Скидка не может быть отрицательной.");
        
        secretDiscount = amount;
    }

    public Product[] GetAllProducts()
    {
        var copy = new Product[products.Count];
        for (var i = 0; i < products.Count; i++)
        {
            copy[i] = products[i];
        }
        return copy;
    }

    private static decimal CalculateDiscountedProductPrice(Product product)
    {
        var discountMultiplier = 0m;
        if ((product.Categories & ProductCategory.Electronics) != 0)
            discountMultiplier += 0.10m;

        if ((product.Categories & ProductCategory.Clothing) != 0)
            discountMultiplier += 0.05m;

        if ((product.Categories & ProductCategory.Food) != 0)
            discountMultiplier += 0.02m;

        return product.Price * (1m - discountMultiplier);
    }
}