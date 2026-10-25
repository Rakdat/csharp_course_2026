using System.Text;

namespace App.Lab2;

public class StackMachine
{
    public static string CalculateString(string[] codeLines) 
    {
        var result = new StringBuilder();
        foreach (var i in codeLines)
        {
            if (i.StartsWith("push"))
            {
                if (i.Length > 5)
                    result.Append(i.AsSpan(5));
            }
            else
            {
                var valuetodelete =  int.Parse(i.AsSpan(4));
                result.Length -= valuetodelete;
            }
        }
        return result.ToString();
    }
}