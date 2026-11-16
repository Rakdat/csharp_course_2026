namespace App.lesson;

public class Coffee
{
    private Dictionary<string, decimal> products = new()
    {
        ["Эспресо"] = 100m,
        ["Капучино"] = 150m,
        ["Круассан"] = 120m
    };

    public decimal GetOrderPrice(List<string> order)
    {
        if (order.Count == 0)
        {
            return 0;
        }
        return 1;
    }
}