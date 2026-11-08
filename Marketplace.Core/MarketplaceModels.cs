using System;
using System.Collections.Generic;

namespace Marketplace.Core;

[Flags]
public enum ProductCategory
{
    None = 0,
    Electronics = 1 << 0,
    Clothing = 1 << 1, 
    Food = 1 << 2,    
    Books = 1 << 3
}

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public ProductCategory Categories { get; }

    public Product(int id, string name, decimal price, ProductCategory categories)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название товара не может быть пустым.", nameof(name));
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Цена не может быть отрицательной.");

        Id = id;
        Name = name;
        Price = price;
        Categories = categories;
    }
}

public class ShoppingCart
{
    private readonly List<Product> _products = new();
    private decimal _secretDiscount;

    public int Count => _products.Count;

    public Product this[int index]
    {
        get
        {
            if (index < 0 || index >= _products.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс находится вне границ корзины.");
            
            return _products[index];
        }
    }

    public decimal TotalPrice
    {
        get
        {
            var sum = 0m;
            foreach (var product in _products)
                sum += CalculateDiscountedProductPrice(product);

            var finalTotal = sum - _secretDiscount;
            return finalTotal > 0m ? Math.Round(finalTotal, 2) : 0m;
        }
    }

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        _products.Add(product);
    }

    public void Clear()
    {
        _products.Clear();
        _secretDiscount = 0m;
    }

    internal void SetDiscount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Скидка не может быть отрицательной.");
        
        _secretDiscount = amount;
    }

    public Product[] GetAllProducts()
    {
        var copy = new Product[_products.Count];
        for (var i = 0; i < _products.Count; i++)
        {
            copy[i] = _products[i];
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

public class MarketingService
{
    public void ApplySecretDiscount(ShoppingCart cart, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(cart);
        cart.SetDiscount(amount);
    }
}