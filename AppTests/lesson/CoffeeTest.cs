using App.lesson;

namespace AppTests.lesson;

public class CoffeeTest
{
    [TestCase(new string[0], 0)]
    [TestCase(new string[] {"Эспресо"}, 100)]
    [TestCase(new string[] {"Эспресо", "Эспресо", "Эспресо"}, 300)]
    public void ValidOrder_Returns_ExpectedPrices(string[] order, decimal expected)
    {
        var coffee = new Coffee();
        Assert.That(coffee.GetOrderPrice(order), Is.EqualTo(expected));
    }

    [TestCase(new string[] { "Эспресо", "Латте" }, new string[] { "Эспресо x1 100", "Латте x1 200" })]

    public void Strings_Out(string[] order, string[] expected)
    {
        var coffee = new Coffee();
        Assert.That(coffee.Get(order), Is.EqualTo(expected));
    }
}