public class Printer
{
    public static void PrintRunning()
    {
        Console.WriteLine("Running product scanner..");
    }
    public static void PrintScanning()
    {
        Console.WriteLine("Scanning products..");
    }
    public static void PrintExiting()
    {
        Console.WriteLine("Exiting product scanner..");
    }
    public static void PrintEnter()
    {
        Console.WriteLine("Enter a letter between A-Z (or 'enter' to quit): ");
    }
    public static void PrintLine()
    {
        Console.WriteLine("----------------------------------------");
    }
    public static void PrintScannedProduct(Product product)
    {
        Console.WriteLine(FillString($"{product.Name.ToUpper()}", 28, false) + 
            FillString($"{product.Price:F2} {Currency.DKK}", 12, true));
    }
    public static void PrintProductDiscount(decimal discount)
    {
        Console.WriteLine(discount > 0 ? FillString("DISCOUNT:", 28, false) + 
            FillString($"-{discount:F2} {Currency.DKK}", 12, true) : "");
    }
    private static List<string> PrintProductCategory(Dictionary<Product, int> products, int category)
    {
        List<string> printStrings = new List<string>();
        Dictionary<Product, int> categoryProducts = products.Where(pair => pair.Key.Category == category)
                                      .ToDictionary(pair => pair.Key, pair => pair.Value);
        if (categoryProducts.Count() > 0)
        {
            printStrings.Add($"\nCATEGORY: {Category.GetName(categoryProducts.First().Key.Category).ToUpper()}");
            foreach (var product in categoryProducts)
            {
                Dictionary<Product, int> productsOfType = products.Where(pair => pair.Key.EAN == product.Key.EAN)
                                      .ToDictionary(pair => pair.Key, pair => pair.Value);

                printStrings.Add(FillString($"{product.Key.Name.ToUpper()} X {product.Value}", 28, false) + 
                    FillString($"{product.Value * product.Key.Price:F2} {Currency.DKK}", 12, true));

                decimal discount = PriceCalculator.GetDiscountTotal(productsOfType);
                if (discount > 0)
                {
                    printStrings.Add(FillString("DISCOUNT:", 28, false) + 
                        FillString($"-{discount:F2} {Currency.DKK}", 12, true));
                }
            }
        }
        return printStrings;
    }
    public static void PrintProductList(Dictionary<Product, int> products)
    {
        List<string> printStrings = new List<string>();
        printStrings.AddRange(PrintProductCategory(products, 1));
        printStrings.AddRange(PrintProductCategory(products, 2));
        printStrings.AddRange(PrintProductCategory(products, 3));
        printStrings.AddRange(PrintProductCategory(products, 4));
        printStrings.AddRange(PrintProductCategory(products, 5));
        printStrings.AddRange(PrintProductCategory(products, 6));
        printStrings.AddRange(PrintProductCategory(products, 7));
        printStrings.AddRange(PrintProductCategory(products, 8));
        printStrings.AddRange(PrintProductCategory(products, 9));
        foreach (string printString in printStrings)
        {
            Console.WriteLine(printString);
        }
        Console.WriteLine("");
    }
    public static void PrintCalculatedPriceTotal(decimal total)
    {
        string totalString = FillString("TOTAL:", 28, false);
        totalString += FillString($"{total:F2} {Currency.DKK}", 12, true);
        Console.WriteLine(totalString);
    }
    public static void PrintNoProductFound(string input)
    {
        Console.WriteLine($"No product found for the letter '{input}'.");
    }
    public static void PrintInvalidInput()
    {
        Console.WriteLine("Invalid input. Please enter a letter between A-Z (or 'enter' to quit).");
    }
    public static void PrintError(Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    private static string FillString(string inputStr, int desiredLength, bool leftSide, char fillCharacter = ' ')
    {
        string cleanInputStr = System.Text.RegularExpressions.Regex.Replace(inputStr, @"\x1b\[\d+m", "");

        if (cleanInputStr.Length >= desiredLength)
        {
            return inputStr;
        }

        int fillCount = desiredLength - cleanInputStr.Length;
        string filledStr;

        if (leftSide)
        {
            filledStr = new string(fillCharacter, fillCount) + inputStr;
        }
        else
        {
            filledStr = inputStr + new string(fillCharacter, fillCount);
        }

        return filledStr;
    }
}
