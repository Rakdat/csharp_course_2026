using App.Lab2;

namespace AppTests.Lab2;

public class LogAggregatorTests
{
    [Test]
    public void MergeLogs_OnlyGarbage_ReturnsEmptyString()
    {
        var result = LogAggregator.MergeLogs("garbage", "[invalid]", null);
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MergeLogs_SingleGroup_SortsByTime()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:02] AuthService: Second"
        });

        Assert.That(result, Is.EqualTo(expected));
    }
}