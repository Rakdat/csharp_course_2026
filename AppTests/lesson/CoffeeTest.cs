using App.lesson;

namespace AppTests.lesson;

public class CoffeeTest
{
    [Test]
    public void EmptyOrder_Returns_Zero()
    {
        var coffee = new Coffee();
        var order = new List<string> ();
        
        Assert.That(coffee.GetOrderPrice(order), Is.EqualTo(0));
    }
}