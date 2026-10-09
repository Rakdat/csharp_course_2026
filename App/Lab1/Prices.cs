namespace App.Lab1;

public static class Prices
{
    public static string GetCurrencyAlias(int price, bool isShorNotation, bool isFirstCapital)
    {
        var lastDigit = price % 10;
        if (isShorNotation)
        {
            return "Руб.";
        }

        string res;
        switch (lastDigit)
        {
            case 1:
                if (price % 100 == 11)
                {
                    res = "рублей";
                }

                res = "рубль";
                break;
            case 2:
                if (price % 100 == 12)
                {
                    res = "рублей";
                }

                res = "рубля";
                break;
            case 3:
                if (price % 100 == 13)
                {
                    res = "рублей";
                }

                res = "рубля";
                break;
            case 4:
                if (price % 100 == 14)
                {
                    res = "рублей";
                }

                res = "рубля";
                break;
            default:
                res = "рублей";
                break;
        }

        if (isFirstCapital)
        {
            res = char.ToUpper(res[0]) + res[1..];
        }

        return res;
    }
}