 
using System.Text;

namespace App.lesson;

public class Coffee
{
    private Dictionary<string, decimal> products = new()
    {
        ["Эспресо"] = 100m,
        ["Капучино"] = 150m,
        ["Круассан"] = 120m,
        ["Латте"] = 200m
    };

    private Dictionary<string, int> currentOrder = new();

    public void Add(string order)
    {
        var parts = order.Split(' ');
        var name = parts[0];
        var count = int.Parse(parts[1]);
        if (count <= 0)
            throw new ArgumentException("Количество товара не может быть меньше либо раавно 0");
        if (!products.ContainsKey(name))
            throw new ArgumentException("Такого товара нет");

        if (currentOrder.ContainsKey(name))
            currentOrder[name] += count;
        else
            currentOrder.Add(name, count);
    }
    
    public decimal GetOrderPrice()
    {
        var sum = 0m;
        foreach (var (name, count) in currentOrder)
        {
            sum += count *  products[name];
        }
        return sum;
    }

    public string[] GetOrder()
    {
        var sb = new StringBuilder();
        var i = 0;
        string[] res = new string[currentOrder.Count];
        foreach (var(name, count) in  currentOrder)
        {
            sb.Append(name).Append($" x{count} ").Append((count * products[name]).ToString());
            res[i++] = sb.ToString();
            sb.Length = 0;
        }
        return res;
    }
}