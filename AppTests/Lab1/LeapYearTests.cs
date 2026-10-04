using App;
using App.Lab1;

namespace AppTests.Lab1;

public class LeapYearTests
{
    //TODO напишите свои тесты
    [TestCase(2100, false)]
    [TestCase(2400, true)]
    public void TestPasses_When_Result_Correct(int year, bool expected)
    {
        var actual = LeapYear.IsLeapYear(year);
        Assert.That(actual, Is.EqualTo(expected));
    }
}