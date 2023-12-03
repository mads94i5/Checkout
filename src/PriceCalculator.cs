public abstract class PriceCalculator
{
    public abstract void Show(Product product);

    public static decimal CalculateTotal(List<Product> products)
    {
        decimal total = 0;
        foreach (Product product in products)
        {
            total += product.Price;
        }
        total -= GetDiscountTotal(GetProductQuantities(products));

        return total;
    }

    protected decimal GetDiscountSingle(Product product, List<Product> products)
    {
        decimal discount = 0;

        if (product.Discount != null)
        {
            (int discountQuantity, decimal discountPrice) = product.Discount;

            Func<Product, bool> productQuantityFilter = p => p.EAN == product.EAN;

            int productQuantity = products.Count(productQuantityFilter);

            if (discountQuantity > 0 && productQuantity % discountQuantity == 0)
            {
                discount = product.Price * discountQuantity - discountPrice;
            }
        }
        return discount;
    }

    public static decimal GetDiscountTotal(Dictionary<Product, int> products)
    {
        decimal discount = 0;

        foreach (var product in products)
        {
            if (product.Key.Discount.Item1 != 0)
            {
                (int discountQuantity, decimal discountPrice) = product.Key.Discount;
                int productDiscountSets = product.Value / discountQuantity;
                discount = (productDiscountSets * product.Key.Price * discountQuantity) - (productDiscountSets * discountPrice);
            }
        }

        return discount;
    }

    protected static Dictionary<Product, int> GetProductQuantities(List<Product> products)
    {
        Dictionary<Product, int> productQuantities = new Dictionary<Product, int>();

        foreach (Product product in products)
        {
            if (!productQuantities.ContainsKey(product))
            {
                productQuantities[product] = 0;
            }
            productQuantities[product]++;
        }
        return productQuantities;
    }
}
