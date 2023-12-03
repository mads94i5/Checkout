public class PriceCalculatorExpensive : PriceCalculator
{
    List<Product> products = new List<Product>();
    public override void Show(Product product)
    {
        products.Add(product);
        Printer.PrintProductDiscount(GetDiscountSingle(product, products));
        Printer.PrintLine();
        Printer.PrintProductList(GetSortedProducts(products));
        Printer.PrintLine();
        Printer.PrintCalculatedPriceTotal(CalculateTotal(products));
        Printer.PrintLine();
    }

    private Dictionary<Product, int> GetSortedProducts(List<Product> products)
    {
        Dictionary<Product, int> productQuantities = GetProductQuantities(products);
        Dictionary<Product, int> sortedProducts = productQuantities.OrderBy(product => product.Key.Category)
                                      .ToDictionary(pair => pair.Key, pair => pair.Value);
        return sortedProducts;
    }
}
