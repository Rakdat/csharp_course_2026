using System;
using NUnit.Framework;
using App.Lab2;

namespace AppTests.Lab2;

[TestFixture]
public class LogAggregatorTests
{
    [Test]
    public void MergeLogs_EmptyOrNullInput_ReturnsEmptyString()
    {
        Assert.That(LogAggregator.MergeLogs(), Is.EqualTo(""), "Должен возвращать пустую строку при отсутствии аргументов.");
        Assert.That(LogAggregator.MergeLogs(null), Is.EqualTo(""), "Должен возвращать пустую строку при null.");
    }

    [Test]
    public void MergeLogs_OnlyGarbage_ReturnsEmptyString()
    {
        var result = LogAggregator.MergeLogs("garbage", "[invalid]", "[2023-10-25] No TraceId", null, "");
        Assert.That(result, Is.EqualTo(""), "Должен игнорировать строки, не подходящие под регулярное выражение.");
    }

    [Test]
    public void MergeLogs_SingleGroup_SortsByTime()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First",
            "[2023-10-25 14:05:03] [trace-A] AuthService: Third"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:02] AuthService: Second",
            "[2023-10-25 14:05:03] AuthService: Third"
        });

        Assert.That(result, Is.EqualTo(expected), "События внутри группы должны быть отсортированы по времени.");
    }

    [Test]
    public void MergeLogs_SameTime_SortsByServiceNameLexicographically()
    {
        // Время одинаковое, отличие только в именах сервисов
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:00] [trace-1] ZService: Last alphabetically",
            "[2023-10-25 14:05:00] [trace-1] AService: First alphabetically"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-1] ---",
            "[2023-10-25 14:05:00] AService: First alphabetically",
            "[2023-10-25 14:05:00] ZService: Last alphabetically"
        });

        Assert.That(result, Is.EqualTo(expected), "При равном времени сортировка должна идти по имени сервиса.");
    }

    [Test]
    public void MergeLogs_MultipleGroups_SortsGroupsByEarliestEvent()
    {
        // trace-B имеет самое раннее событие (14:05:00)
        // trace-A начинается позже (14:05:01)
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:01] [trace-A] AuthService: User logged in",
            "[2023-10-25 14:05:05] [trace-B] BillingService: Charge attempted",
            "[2023-10-25 14:05:02] [trace-A] AuthService: Token generated",
            "[2023-10-25 14:05:00] [trace-B] Gateway: Request received"
        );

        var expected = string.Join(Environment.NewLine + Environment.NewLine, new[]
        {
            string.Join(Environment.NewLine, new[]
            {
                "--- TraceId: [trace-B] ---",
                "[2023-10-25 14:05:00] Gateway: Request received",
                "[2023-10-25 14:05:05] BillingService: Charge attempted"
            }),
            string.Join(Environment.NewLine, new[]
            {
                "--- TraceId: [trace-A] ---",
                "[2023-10-25 14:05:01] AuthService: User logged in",
                "[2023-10-25 14:05:02] AuthService: Token generated"
            })
        });

        Assert.That(result, Is.EqualTo(expected), "Группы должны сортироваться по самому раннему событию внутри них.");
    }

    [Test]
    public void MergeLogs_MixOfValidAndInvalidLogs_ProcessesOnlyValid()
    {
        var result = LogAggregator.MergeLogs(
            "This is just some random text",
            "[2023-10-25 14:00:00] [trace-1] GoodService: Valid log",
            "[2023-99-99 14:00:00] [trace-2] BadService: Invalid date format", // Предполагается, что регулярка строгая
            "[2023-10-25 14:00:01] [trace-1] GoodService: Valid log 2",
            "[2023-10-25 14:00:02] MissingBrackets Service: Fail"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-1] ---",
            "[2023-10-25 14:00:00] GoodService: Valid log",
            "[2023-10-25 14:00:01] GoodService: Valid log 2"
        });

        Assert.That(result, Is.EqualTo(expected), "Невалидные логи должны игнорироваться, а валидные обрабатываться корректно.");
    }
}