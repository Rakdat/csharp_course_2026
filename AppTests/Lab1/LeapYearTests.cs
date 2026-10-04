using App;
using App.Lab1;

namespace AppTests.Lab1;

public class LeapYearTests
{
    //TODO напишите свои тесты
    [TestCase(2100, false)]
    [TestCase(2400, true)]
    [TestCase(2200, false)]
    [TestCase(2020, true)]
    [TestCase(2000, true)]
    [TestCase(1000, false)]
    [TestCase(4, true)]
    [TestCase(1200, true)]
    [TestCase(203, false)]
    [TestCase(1900, false)]
    public void TestPasses_When_Result_Correct(int year, bool expected)
    {
        var actual = LeapYear.IsLeapYear(year);
        Assert.That(actual, Is.EqualTo(expected));
    }
}