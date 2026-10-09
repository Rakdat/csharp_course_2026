namespace App.Lab1;

public enum PaymentsPlan
{
    Differentiated,
    Annuity
}

public static class Payments
{
    public static decimal Degree(decimal body, int count)
    {
        var returnvalue = 1m;
        for (int i = 0; i < count; i++)
        {
            returnvalue = returnvalue * body;
        }
        return returnvalue;
    }

    public static decimal CalculateTotalPayments(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount)
    {
        if (rate == 0 || monthsCount == 0)
        {
            return amount;
        }

        var newrate = rate / 1200m;
        var sumOfPayments = 0m;
        if (plan == PaymentsPlan.Differentiated)
        {
            var payment = amount / monthsCount;
            var rest = amount;
            for (int i = 0; i < monthsCount; i++)
            {
                sumOfPayments += payment + rest * newrate;
                rest -= payment;
            }
        }
        else if (plan == PaymentsPlan.Annuity)
        {
            var power = Degree(1 + newrate, monthsCount);
            sumOfPayments = monthsCount * (amount * ((newrate * power) / (power - 1)));
        }

        return sumOfPayments;
    }
}