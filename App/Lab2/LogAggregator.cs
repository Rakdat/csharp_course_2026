using System.Globalization;

namespace App.Lab2;

public class LogAggregator
{
    private static (string TraceId, string NewMessage) ParseLogLine(string message)
    {
        if (string.IsNullOrEmpty(message) || message.Length < 30)
            return (string.Empty, string.Empty);

        if (message[0] != '[' || message[20] != ']' || message[21] != ' ' || message[22] != '[')
            return (string.Empty, string.Empty);
        
        if (!DateTime.TryParseExact(
                message.AsSpan(1, 19),
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
        {
            return (string.Empty, string.Empty);
        }

        var traceIdEnd = message.IndexOf(']', 23);
        if (traceIdEnd <= 23)
            return (string.Empty, string.Empty);

        for (var i = 23; i < traceIdEnd; i++)
        {
            var c = message[i];
            if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-'))
                return (string.Empty, string.Empty);
        }

        if (traceIdEnd + 1 >= message.Length || message[traceIdEnd + 1] != ' ')
            return (string.Empty, string.Empty);

        var serviceStart = traceIdEnd + 2;
        var serviceEnd = message.IndexOf(':', serviceStart);
        if (serviceEnd <= serviceStart)
            return (string.Empty, string.Empty);

        for (var i = serviceStart; i < serviceEnd; i++)
        {
            var c = message[i];
            if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '_'))
                return (string.Empty, string.Empty);
        }

        if (serviceEnd + 2 >= message.Length || message[serviceEnd + 1] != ' ')
            return (string.Empty, string.Empty);
        
        var traceId = message.Substring(23, traceIdEnd - 23);
        var newMessage = message.Remove(21, traceIdEnd - 20);

        return (traceId, newMessage);
    }
    public static string MergeLogs(params string[] logLines)
    {
        if (logLines == null || logLines.Length == 0) return "";
        
        var dict = new Dictionary<string, List<string>>();
        foreach (var message in logLines)
        {
            var (traceId, newMessage) = ParseLogLine(message);
            if (string.IsNullOrEmpty(traceId)) continue;
            
            if (!dict.TryGetValue(traceId, out var value))
            {
                value = [];
                dict.Add(traceId, value);
            }
            value.Add(newMessage);
        }
        
        if (dict.Count == 0) return "";
        
        foreach (var list in dict.Values)
        {
            list.Sort();
        }
        
        var sortedGroups = dict.OrderBy(group => group.Value[0], StringComparer.Ordinal);

        var resultBlocks = new List<string>();
        foreach (var group in sortedGroups)
        {
            var blockLines = new List<string> { $"--- TraceId: [{group.Key}] ---" };
            blockLines.AddRange(group.Value);
            
            resultBlocks.Add(string.Join(Environment.NewLine, blockLines));
        }

        return string.Join(Environment.NewLine + Environment.NewLine, resultBlocks);
    }
}