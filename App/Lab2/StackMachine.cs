using System.Text;

namespace App.Lab2;

public class StackMachine
{
    public static string CalculateString(string[] codeLines) 
    {
        var result = string.Empty;
        foreach (var i in codeLines)
        {
            if (i.StartsWith("push"))
                result +=  i.Length > 5 ? i[5..] : "";
            else if (i.StartsWith("pop"))
            {
                var valuetodelete =  int.Parse(i[4..]);
                result = result[..^valuetodelete];
            }
        }
        return result;
    }
}