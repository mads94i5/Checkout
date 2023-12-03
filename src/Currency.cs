enum Currency
{
    DKK,
    EUR
}
static class CurrencyExtensions
{
    public static string GetSymbol(this Currency currency)
    {
        switch (currency)
        {
            case Currency.DKK:
                return "kr.";
            case Currency.EUR:
                return "€";
            default:
                throw new ArgumentOutOfRangeException(nameof(currency), currency, null);
        }
    }
}