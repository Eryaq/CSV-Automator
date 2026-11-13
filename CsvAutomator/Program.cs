using System.Globalization;

string inputDirectory = Path.Combine("Data", "Input");

if (!Directory.Exists(inputDirectory))
{
    Console.WriteLine($"Input directory not found: {inputDirectory}");
    return;
}

string[] csvFiles = Directory.GetFiles(inputDirectory, "*.csv").OrderBy(path => path, StringComparer.OrdinalIgnoreCase).ToArray();

if (csvFiles.Length == 0)
{
    Console.WriteLine("No CSV files found.");
    return;
}

List<Order> orders = new();

Console.WriteLine($"Found {csvFiles.Length} CSV file(s).");

foreach (string csvFile in csvFiles)
{
    Console.WriteLine($"\nProcessing: {Path.GetFileName(csvFile)}");

    string[] lines;

    try
    {
        lines = File.ReadAllLines(csvFile);
    }
    catch (IOException exception)
    {
        Console.WriteLine($"Error reading file: {exception.Message}");
        continue;
    }
    catch (UnauthorizedAccessException exception)
    {
        Console.WriteLine($"Access denied: {exception.Message}");
        continue;
    }

    for (int i = 1; i < lines.Length; i++)
    {
        string[] columns = lines[i].Split(',');

        if (columns.Length != 3)
        {
            Console.WriteLine($"Warning: Invalid column count on line {i + 1}.");
            continue;
        }

        if (!int.TryParse(columns[0].Trim(), out int orderId) || orderId <= 0)
        {
            Console.WriteLine($"Warning: Invalid OrderId on line {i + 1}.");
            continue;
        }

        string customer = columns[1].Trim();

        if (string.IsNullOrWhiteSpace(customer))
        {
            Console.WriteLine($"Warning: Empty Customer on line {i + 1}.");
            continue;
        }

        if (!decimal.TryParse(columns[2].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount) || amount < 0)
        {
            Console.WriteLine($"Warning: Invalid Amount on line {i + 1}.");
            continue;
        }

        Order order = new()
        {
            OrderId = orderId,
            Customer = customer,
            Amount = amount
        };

        orders.Add(order);

        Console.WriteLine($"Order {order.OrderId}: {order.Customer} - {order.Amount:F2} CZK");
    }
}

Console.WriteLine($"\nTotal valid orders: {orders.Count}");

if (orders.Count > 0)
{
    decimal totalRevenue = orders.Sum(order => order.Amount);
    decimal averageOrder = orders.Average(order => order.Amount);

    Order largestOrder = orders.OrderByDescending(order => order.Amount).First();

    Console.WriteLine("\n===== SALES REPORT =====");
    Console.WriteLine($"Total revenue: {totalRevenue:F2} CZK");
    Console.WriteLine($"Average order: {averageOrder:F2} CZK");
    Console.WriteLine($"Largest order: {largestOrder.Amount:F2} CZK " + $"({largestOrder.Customer}, ID: {largestOrder.OrderId})");

    // Create the output directory if it does not exist.
    string outputDirectory = Path.Combine("Data", "Output");
    Directory.CreateDirectory(outputDirectory);

    string reportPath = Path.Combine(outputDirectory, "sales_report.csv");

    // Generate the CSV report using invariant number formatting.
    string[] reportLines =
    {
        "Metric,Value",
        $"TotalOrders,{orders.Count}",
        $"TotalRevenue,{totalRevenue.ToString("F2", CultureInfo.InvariantCulture)}",
        $"AverageOrder,{averageOrder.ToString("F2", CultureInfo.InvariantCulture)}",
        $"LargestOrder,{largestOrder.Amount.ToString("F2", CultureInfo.InvariantCulture)}",
        $"LargestOrderCustomer,{largestOrder.Customer}",
        $"LargestOrderId,{largestOrder.OrderId}"
    };

    try
    {
        File.WriteAllLines(reportPath, reportLines);
        Console.WriteLine($"\nReport exported successfully: {reportPath}");
    }
    catch (IOException exception)
    {
        Console.WriteLine($"Error exporting report: {exception.Message}");
    }
    catch (UnauthorizedAccessException exception)
    {
        Console.WriteLine($"Access denied when exporting report: {exception.Message}");
    }

}
else
{
    Console.WriteLine("No valid orders available for the report.");
}

