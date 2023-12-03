
public class Scanner
{
    public delegate void ProductScannedEventHandler(Product product);
    public event ProductScannedEventHandler ProductScanned;
    protected virtual void OnProductScanned(Product product)
    {
        ProductScanned?.Invoke(product);
    }

    public void Scan()
    {
        Printer.PrintScanning();

        while (true)
        {
            Printer.PrintEnter();
            string input = Console.ReadLine().ToUpper();

            if (input == string.Empty || input == " ")
            {
                Printer.PrintExiting();
                break;
            }

            if (char.TryParse(input, out _) && char.IsLetter(input.ToCharArray()[0]))
            {
                Product product = GetProductByEAN(input);

                if (product != null)
                {
                    ScanProduct(product);
                }
                else
                {
                    Printer.PrintNoProductFound(input);
                }
            }
            else
            {
                Printer.PrintInvalidInput();
            }
        }
    }

    private void ScanProduct(Product product)
    {
        Thread.Sleep(500);
        Console.Clear();

        Printer.PrintLine();
        Printer.PrintScannedProduct(product);
        OnProductScanned(product);
    }

    private Product GetProductByEAN(string EAN)
    {
        List<string[]> productList = CSVReader.Read("products");
        for (int i = 0; i < productList.Count; i++)
        {
            if (EAN == productList[i][0])
            {
                decimal.TryParse(productList[i][2], out decimal price);
                int.TryParse(productList[i][3], out int discountQuantity);
                decimal.TryParse(productList[i][4], out decimal discountAmount);
                decimal.TryParse(productList[i][5], out decimal deposit);
                int.TryParse(productList[i][6], out int pieces);
                int.TryParse(productList[i][7], out int category);

                Product product = new Product
                {
                    EAN = productList[i][0],
                    Name = productList[i][1],
                    Price = price,
                    Discount = new Tuple<int, decimal>(
                        discountQuantity,
                        discountAmount),
                    Deposit = deposit,
                    Pieces = pieces,
                    Category = category
                };

                return product;
            }
        }
        return null;
    }
}