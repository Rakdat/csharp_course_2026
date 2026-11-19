using Marketplace.Core;

namespace Marketplace.App;

public class Program
{
    public static void Main()
    {
        // Создаем пользователя из предыдущего задания
        var buyer = UserCreator.CreateUser("shopper_max", "pass123", "Макс", "Токарев", "123456", "89130125163");

        // 1. Создаем товары
        var laptop = new Product("Ноутбук", 100000m, ProductCategory.Electronics);
        var smartWatch = new Product("Умные часы", 20000m, ProductCategory.Electronics | ProductCategory.Clothing);
        var apple = new Product("Яблоки", 500m, ProductCategory.Food);

        // Передаем владельца в корзину покупок
        var cart = new ShoppingCart(buyer);
        cart.AddProduct(laptop);     
        cart.AddProduct(smartWatch); 
        cart.AddProduct(apple);      

        Console.WriteLine($"Владелец корзины: {cart.Owner.GetUserFullName()}");
        Console.WriteLine($"Первая позиция в корзине: {cart[0].Name}");
        Console.WriteLine($"Сумма с учетом скидок по категориям: {cart.TotalPrice} руб."); // Ожидается: 107 490

        // А через сервис маркетинга — работает:
        var marketing = new MarketingService();
        marketing.ApplySecretDiscount(cart, 7490m);
        Console.WriteLine($"Сумма после применения секретной скидки: {cart.TotalPrice} руб."); // Ожидается: 100 000

        // 3. Проверка защитного копирования (GetAllProducts)
        Product[] productsSnapshot = cart.GetAllProducts();
        productsSnapshot[0] = null!; // Пытаемся "сломать" массив снаружи

        Console.WriteLine($"Товар в корзине после взлома внешнего массива: {cart[0].Name}"); // Все еще "Ноутбук"!
    }
}