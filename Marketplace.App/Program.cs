using Marketplace.Core;

namespace Marketplace.App;

public class Program
{
    public static void Main()
    {
        // 1. Создаем неизменяемые товары (умные часы относятся сразу к двум категориям через побитовое ИЛИ)
        var laptop = new Product(1, "Ноутбук", 100000m, ProductCategory.Electronics);
        var smartWatch = new Product(2, "Умные часы", 20000m, ProductCategory.Electronics | ProductCategory.Clothing);
        var apple = new Product(3, "Яблоки", 500m, ProductCategory.Food);

        // Попытка изменить цену товара не скомпилируется (свойство только для чтения):
        // laptop.Price = 500m; // Ошибка компиляции!

        var cart = new ShoppingCart();
        cart.AddProduct(laptop);     // 100 000 - 10% (Electronics) = 90 000
        cart.AddProduct(smartWatch); // 20 000 - 15% (10% Electronics + 5% Clothing) = 17 000
        cart.AddProduct(apple);      // 500 - 2% (Food) = 490

        Console.WriteLine($"Первая позиция в корзине (через индексатор): {cart[0].Name}");
        Console.WriteLine($"Сумма с учетом скидок по категориям: {cart.TotalPrice} руб."); // Ожидается: 107 490

        // 2. Попытка вызвать internal-метод напрямую из другого проекта не скомпилируется:
        // cart.SetDiscount(5000m); // Ошибка CS0122: 'ShoppingCart.SetDiscount(decimal)' недоступен из-за уровня защиты.

        // А через сервис маркетинга — работает:
        var marketing = new MarketingService();
        marketing.ApplySecretDiscount(cart, 7490m);
        Console.WriteLine($"Сумма после применения секретной скидки: {cart.TotalPrice} руб."); // Ожидается: 100 000

        // 3. Проверка защитного копирования (GetAllProducts)
        Product[] productsSnapshot = cart.GetAllProducts();
        productsSnapshot[0] = null!; // Пытаемся "сломать" массив снаружи, затерев ноутбук

        Console.WriteLine($"Товар в корзине после взлома внешнего массива: {cart[0].Name}"); // Все еще "Ноутбук"!
    }
}