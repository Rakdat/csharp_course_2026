using System.Text.RegularExpressions;

namespace App.Lab2;

public class LogAggregator
{
    private static readonly Regex LogPattern = new Regex(
        @"^\[\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]) ([01]\d|2[0-3]):[0-5]\d:[0-5]\d\] \[[a-zA-Z0-9-]+\] [a-zA-Z0-9_]+: .+$",
        RegexOptions.Compiled
    );

    private static (string TraceId, string NewMessage) ParseLogLine(string message)
    {
        var firstCloseBracket = message.IndexOf(']');
        var traceIdStart = firstCloseBracket + 3;
        var traceIdEnd = message.IndexOf(']', traceIdStart);
        var traceId = message.Substring(traceIdStart, traceIdEnd - traceIdStart);
        var newMessage = message.Remove(firstCloseBracket + 1, traceIdEnd - firstCloseBracket);
        return (traceId, newMessage);
    }
    public static string MergeLogs(params string[] logLines)
    {
        if (logLines == null || logLines.Length == 0) return "";
        
        const string logMessageFormat = @"^\[\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]) ([01]\d|2[0-3]):[0-5]\d:[0-5]\d\] \[[a-zA-Z0-9-]+\] [a-zA-Z0-9_]+: .+$";
        var dict = new Dictionary<string, List<string>>();
        foreach (var message in logLines)
        {
            if (string.IsNullOrEmpty(message) || !LogPattern.IsMatch(message)) continue;
            
            var (traceId, newMessage) = ParseLogLine(message);

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