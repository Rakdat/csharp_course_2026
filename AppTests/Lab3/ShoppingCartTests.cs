using Marketplace.Core;

namespace AppTests.Lab3;

public class ShoppingCartTests
{
    private User testUser;

    [SetUp]
    public void Setup()
    {
        testUser = new User();
    }

    [TestCase(ProductCategory.None, 1000, 1000, TestName = "CategoryDiscount_None_FullPrice")]
    [TestCase(ProductCategory.Electronics, 1000, 900, TestName = "CategoryDiscount_Electronics_10PercentOff")]
    [TestCase(ProductCategory.Clothing, 1000, 950, TestName = "CategoryDiscount_Clothing_5PercentOff")]
    [TestCase(ProductCategory.Food, 1000, 980, TestName = "CategoryDiscount_Food_2PercentOff")]
    [TestCase(ProductCategory.Electronics | ProductCategory.Clothing, 1000, 850, TestName = "CategoryDiscount_CombinedFlags_15PercentOff")]
    public void TotalPrice_WithCategoryFlags_AppliesCorrectDiscount(ProductCategory category, decimal rawPrice, decimal expectedTotal)
    {
        var cart = new ShoppingCart(testUser);
        cart.AddProduct(new Product("TestItem", rawPrice, category));

        Assert.That(cart.TotalPrice, Is.EqualTo(expectedTotal));
    }

    [Test]
    public void GetAllProducts_WhenModifiedExternally_DoesNotAffectShoppingCart()
    {
        var cart = new ShoppingCart(testUser);
        var originalProduct = new Product("GPU", 50000m, ProductCategory.Electronics);
        cart.AddProduct(originalProduct);

        // Получаем копию массива и пытаемся затереть элемент
        Product[] externalArray = cart.GetAllProducts();
        externalArray[0] = new Product("HackedItem", 0m, ProductCategory.None);

        // Внутри корзины должен остаться оригинальный товар
        Assert.Multiple(() =>
        {
            Assert.That(cart[0].Name, Is.EqualTo("GPU"));
            Assert.That(cart.TotalPrice, Is.EqualTo(45000m));
        });
    }
}