using App.lesson;

namespace AppTests.lesson;

public class CoffeeTest
{
    [TestCase(new string[0], 0)]
    [TestCase(new string[] { "Эспресо 1" }, 100)]
    [TestCase(new string[] { "Капучино 2", "Круассан 3" }, 660)]
    public void ValidOrder_Returns_ExpectedPrices(string[] items, decimal expectedTotal)
    {
        var coffee = new Coffee();
        
        foreach (var item in items)
            coffee.Add(item);
        
        Assert.That(coffee.GetOrderPrice(), Is.EqualTo(expectedTotal));
    }

    [TestCase(new string[] { "Эспресо 2", "Латте -3", "Круассан 1" }, 320)]
    public void Add_WithNegativeCount_SkipsInvalidItem_AndCalculatesTotal(string[] items, decimal expectedTotal)
    {
        var coffee = new Coffee();
        var errorCaught = false;

        foreach (var item in items)
        {
            try
            {
                coffee.Add(item);
            }
            catch (ArgumentException ex)
            {
                errorCaught = true; 
            }
        }
        
        Assert.That(coffee.GetOrderPrice(), Is.EqualTo(expectedTotal));
        Assert.That(errorCaught, Is.True, "Поймали исключение на отрицательном количестве");
    }

    [TestCase(new string[] { "Эспресо 1", "Капучино 1" }, new string[] { "Эспресо x1 100", "Капучино x1 150" })]
    [TestCase(new string[] { "Круассан 2" }, new string[] { "Круассан x2 240" })]
    public void GetReceipt_Returns_FormattedStrings(string[] items, string[] expectedReceipt)
    {
        var coffee = new Coffee();
        foreach (var item in items)
        {
            coffee.Add(item);
        }
        
        Assert.That(coffee.GetOrder(), Is.EqualTo(expectedReceipt));
    }
    
    [TestCase(new string[] { "AAA 1", "Капучино 1" }, new string[] { "Капучино x1 150" })]
    public void ValidateIncorrectName(string[] items, string[] expected)
    {
        var coffee = new Coffee();
        var errorCaught = false;

        foreach (var item in items)
        {
            try
            {
                coffee.Add(item);
            }
            catch (ArgumentException ex)
            {
                errorCaught = true; 
            }
        }
        Assert.That(coffee.GetOrder(), Is.EqualTo(expected));
        Assert.That(errorCaught, Is.True, "Поймали исключение на неправильном имени");
    }
}