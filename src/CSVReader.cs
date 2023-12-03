public static class CSVReader
{
    public static List<string[]> Read(string filename)
    {
        List<string[]> list = new List<string[]>();
        string csvFileName = $"csv/{filename}.csv";
        string workingDirectory = Environment.CurrentDirectory;
        string currentDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        string csvFilePath = Path.Combine(currentDirectory, csvFileName);
        try
        {
            if (!File.Exists(csvFilePath))
            {
                throw new FileNotFoundException($"CSV file not found at path: {csvFilePath}");
            }

            using (var reader = new StreamReader(csvFilePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string dataLine = reader.ReadLine()!;

                    if (dataLine != null)
                    {
                        string[] data = dataLine.Split(",");
                        list.Add(data);
                    }
                }

                if (list.Count == 0)
                {
                    throw new Exception($"No data found in the CSV file: {csvFilePath}");
                }

                return list;
            }
        }
        catch (Exception ex)
        {
            Printer.PrintError(ex);
            Environment.Exit(1);
            return null;
        }
    }
}
