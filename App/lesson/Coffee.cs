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

    public string[] Get(string[] order)
    {
        var sb = new StringBuilder();
        var dict = new Dictionary<string, int>();
        foreach (var prod in order)
        {
            if(dict.ContainsKey(prod))
            {
                dict[prod]++;
            }
            else
            {
                dict.Add(prod, 1);
            }
        }

        var i = 0;
        string[] res = new string[dict.Count];
        foreach (var(name, count) in  dict)
        {
            sb.Append(name).Append($" x{count} ").Append((count * products[name]).ToString());
            res[i++] = sb.ToString();
            sb.Length = 0;
        }
        return res;
    }
}