using App;
using App.Lab1;

namespace AppTests.Lab1;

public class PricesTests
{
    //TODO напишите свои тесты
    [TestCase(1000052, "рубля")]
    [TestCase(25, "рублей")]
    public void TestPasses_When_Result_Correct(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, false);
        Assert.That(actual, Is.EqualTo(expected));
    }
}