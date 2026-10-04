using App;
using App.Lab1;

namespace AppTests.Lab1;

public class PaymentsTests
{
    //TODO напишите свои тесты
    [TestCase(PaymentsPlan.Annuity, 7, 3, 10000, 10116.90)]
    [TestCase(PaymentsPlan.Differentiated, 3, 5, 200000, 201500)]
    public void TestPasses_When_Result_Correct(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount, decimal expected)
    {
        var actual = Payments.CalculateTotalPayments(plan, rate, monthsCount, amount);
        Assert.That(actual, Is.EqualTo(expected));
    }
}