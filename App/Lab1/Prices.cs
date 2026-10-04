namespace App.Lab1;

public static class Prices
{
    public static string GetCurrencyAlias(int price, bool isShorNotation, bool isFirstCapital)
    {
        var lastDigit = price % 10;

        switch (lastDigit)
        {
            case 1:
                if (price % 100 == 11)
                {
                    return "рублей";
                }

                return "рубль";
            case 2:
                if (price % 100 == 12)
                {
                    return "рублей";
                }

                return "рубля";
            case 3:
                if (price % 100 == 13)
                {
                    return "рублей";
                }

                return "рубля";
            case 4:
                if (price % 100 == 14)
                {
                    return "рублей";
                }

                return "рубля";
            default:
                return "рублей";
        }
    }
}