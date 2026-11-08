using Marketplace.Core;

namespace AppTests.Lab3;

public class MarketplaceTests
{
    [TestCase(ProductCategory.None, 1000, 1000, TestName = "CategoryDiscount_None_FullPrice")]
    [TestCase(ProductCategory.Electronics, 1000, 900, TestName = "CategoryDiscount_Electronics_10PercentOff")]
    [TestCase(ProductCategory.Clothing, 1000, 950, TestName = "CategoryDiscount_Clothing_5PercentOff")]
    [TestCase(ProductCategory.Food, 1000, 980, TestName = "CategoryDiscount_Food_2PercentOff")]
    [TestCase(ProductCategory.Electronics | ProductCategory.Clothing, 1000, 850, TestName = "CategoryDiscount_CombinedFlags_15PercentOff")]
    public void TotalPrice_WithCategoryFlags_AppliesCorrectDiscount(ProductCategory category, decimal rawPrice, decimal expectedTotal)
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product(1, "TestItem", rawPrice, category));

        Assert.That(cart.TotalPrice, Is.EqualTo(expectedTotal));
    }

    [Test]
    public void MarketingService_ApplySecretDiscount_ReducesTotalPrice()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product(1, "Phone", 10000m, ProductCategory.None));
        var marketing = new MarketingService();

        marketing.ApplySecretDiscount(cart, 1500m);

        Assert.That(cart.TotalPrice, Is.EqualTo(8500m));
    }

    [Test]
    public void TotalPrice_WhenDiscountExceedsPrice_ReturnsZeroNotNegative()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product(1, "Bread", 100m, ProductCategory.None));
        var marketing = new MarketingService();

        marketing.ApplySecretDiscount(cart, 500m);

        Assert.That(cart.TotalPrice, Is.EqualTo(0m));
    }

    [Test]
    public void GetAllProducts_WhenModifiedExternally_DoesNotAffectShoppingCart()
    {
        var cart = new ShoppingCart();
        var originalProduct = new Product(1, "GPU", 50000m, ProductCategory.Electronics);
        cart.AddProduct(originalProduct);

        // Получаем копию массива и пытаемся затереть элемент
        Product[] externalArray = cart.GetAllProducts();
        externalArray[0] = new Product(999, "HackedItem", 0m, ProductCategory.None);

        // Внутри корзины должен остаться оригинальный товар
        Assert.Multiple(() =>
        {
            Assert.That(cart[0].Id, Is.EqualTo(1));
            Assert.That(cart[0].Name, Is.EqualTo("GPU"));
            Assert.That(cart.TotalPrice, Is.EqualTo(45000m));
        });
    }
}