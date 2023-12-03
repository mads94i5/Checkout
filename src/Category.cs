public static class Category
{
    public static string GetName(int index)
    {
        return index switch
        {
            1 => "Dairy Products",
            2 => "Non-Perishable",
            3 => "Fresh Produce",
            4 => "Bakery and Bread",
            5 => "Frozen Foods",
            6 => "Meat and Poultry",
            7 => "Beverages",
            8 => "Snacks",
            9 => "Household Items",
            _ => "Unknown Category",
        };
    }
}