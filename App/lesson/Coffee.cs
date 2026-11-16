namespace App.lesson;

public class Coffee
{
    private Dictionary<string, decimal> products = new()
    {
        ["Эспресо"] = 100m,
        ["Капучино"] = 150m,
        ["Круассан"] = 120m
    };

    public decimal GetOrderPrice(string[] order)
    {
        if (order.Length == 0)
        {
            return 0;
        }
        var sum = 0m;
        foreach (var item in order)
        {
            sum += products[item];
        }
        return sum;
    }
}