using App.Lab1;

namespace AppTests.Lab1;

public class PaymentsTests
{
    //TODO напишите свои тесты
    [TestCase(PaymentsPlan.Annuity, 7, 3, 10000, 10116.89)]
    [TestCase(PaymentsPlan.Differentiated, 3, 5, 200000, 201500)]
    // Базовые сценарии
    [TestCase(PaymentsPlan.Differentiated, 12, 12, 120000, 127800)] // 12% годовых, 12 мес
    [TestCase(PaymentsPlan.Differentiated, 10, 6, 60000, 61750)]    // 10% годовых, 6 мес
    [TestCase(PaymentsPlan.Differentiated, 24, 5, 100000, 106000)]  // 24% годовых, 5 мес
    // Базовые сценарии
    [TestCase(PaymentsPlan.Annuity, 12, 12, 120000, 127942.26)]
    [TestCase(PaymentsPlan.Annuity, 24, 5, 100000, 106079.2)]
    [TestCase(PaymentsPlan.Annuity, 15, 24, 500000, 581839.78)]
    // Беспроцентная рассрочка (ставка 0%) - долг не должен измениться
    [TestCase(PaymentsPlan.Annuity, 0, 6, 50000, 50000)]
    [TestCase(PaymentsPlan.Differentiated, 0, 12, 100000, 100000)]

    // Кредит на 1 месяц (аннуитет и дифференцированный должны быть равны)
    [TestCase(PaymentsPlan.Annuity, 12, 1, 10000, 10100)]
    [TestCase(PaymentsPlan.Differentiated, 12, 1, 10000, 10100)]
    
    [TestCase(PaymentsPlan.Differentiated, -12, 1, 10000, 10100)]
    public void TestPasses_When_Result_Correct(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount, decimal expected)
    {
        var actual = Payments.CalculateTotalPayments(plan, rate, monthsCount, amount);
        actual = Math.Round(actual, 2);
        Assert.That(actual, Is.EqualTo(expected));
    }
}