public class Program
{
    public static void Main()
    {
        Program program = new Program();
        program.Run();
    }

    private void Run()
    {
        Printer.PrintRunning();
        Scanner scanner = new Scanner();

        //PriceCalculatorCheap cheapCalculator = new PriceCalculatorCheap();
        //scanner.ProductScanned += cheapCalculator.Show;

        PriceCalculatorExpensive expensiveCalculator = new PriceCalculatorExpensive();
        scanner.ProductScanned += expensiveCalculator.Show;

        scanner.Scan();
    }
}