using App.lesson;

namespace AppTests.lesson;

public class CoffeeTest
{
    [TestCase(new string[0], 0)]
    [TestCase(new string[] {"Эспресо"}, 100)]
    public void ValidOrder_Returns_ExpectedPrices(string[] order, decimal expected)
    {
        var coffee = new Coffee();
        Assert.That(coffee.GetOrderPrice(order), Is.EqualTo(expected));
    }
}