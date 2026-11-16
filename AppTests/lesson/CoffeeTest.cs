using App.lesson;

namespace AppTests.lesson;

public class CoffeeTest
{
    [TestCase(new string[0], 0)]
    [TestCase(new string[] { "эспресо 1" }, 100)]
    [TestCase(new string[] { "капучино 2", "круассан 3" }, 660)]
    public void ValidOrder_Returns_ExpectedPrices(string[] items, decimal expectedTotal)
    {
        var coffee = new Coffee();
        
        foreach (var item in items)
            coffee.Add(item);
        
        Assert.That(coffee.GetOrderPrice(), Is.EqualTo(expectedTotal));
    }

    [TestCase(new string[] { "эспресо 2", "латте -3", "круассан 1" }, 320)]
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

    [TestCase(new string[] { "эспресо 1", "капучино 1" }, new string[] { "эспресо x1 100", "капучино x1 150" })]
    [TestCase(new string[] { "круассан 2" }, new string[] { "круассан x2 240" })]
    public void GetReceipt_Returns_FormattedStrings(string[] items, string[] expectedReceipt)
    {
        var coffee = new Coffee();
        foreach (var item in items)
        {
            coffee.Add(item);
        }
        
        Assert.That(coffee.GetOrder(), Is.EqualTo(expectedReceipt));
    }
    
    [TestCase(new string[] { "AAA 1", "капучино 1" }, new string[] { "капучино x1 150" })]
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

    [TestCase(new string[] { "КаПучино 1", "лаТте 2" }, new string[] { "капучино x1 150", "латте x2 400" })]
    public void RegisterIndependent(string[] items, string[] expected)
    {
        var coffee = new Coffee();
        foreach (var item in items)
            coffee.Add(item);
        Assert.That(coffee.GetOrder(), Is.EqualTo(expected));
    }
    
    [TestCase(new string[] { "КаПучино 1", "КаПучино 1" }, new string[] {"капучино x2 300"})]
    public void AddTheSame(string[] items, string[] expected)
    {
        var coffee = new Coffee();
        foreach (var item in items)
            coffee.Add(item);
        Assert.That(coffee.GetOrder(), Is.EqualTo(expected));
    }
}