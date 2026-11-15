
using Marketplace.Core;

namespace AppTests.Lab3;

public class MarketingServiceTests
{
    private User testUser;

    [SetUp]
    public void Setup()
    {
        testUser = new User(); 
    }

    [Test]
    public void MarketingService_ApplySecretDiscount_ReducesTotalPrice()
    {
        var cart = new ShoppingCart(testUser);
        cart.AddProduct(new Product("Phone", 10000m, ProductCategory.None));
        var marketing = new MarketingService();

        marketing.ApplySecretDiscount(cart, 1500m);

        Assert.That(cart.TotalPrice, Is.EqualTo(8500m));
    }

    [Test]
    public void TotalPrice_WhenDiscountExceedsPrice_ReturnsZeroNotNegative()
    {
        var cart = new ShoppingCart(testUser);
        cart.AddProduct(new Product("Bread", 100m, ProductCategory.None));
        var marketing = new MarketingService();

        marketing.ApplySecretDiscount(cart, 500m);

        Assert.That(cart.TotalPrice, Is.EqualTo(0m));
    }
}