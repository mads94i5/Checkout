public class PriceCalculatorCheap : PriceCalculator
{
    List<Product> products = new List<Product>();
    public override void Show(Product product)
    {
        products.Add(product);
        Printer.PrintProductDiscount(GetDiscountSingle(product, products));
        Printer.PrintLine();
        Printer.PrintCalculatedPriceTotal(CalculateTotal(products));
        Printer.PrintLine();
    }
}