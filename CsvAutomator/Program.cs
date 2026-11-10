using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

Console.WriteLine("=== CSV Automator ===");
Console.WriteLine("Version 0.3.0");
Console.WriteLine();

string csvPath = Path.Combine("Data", "orders.csv");

if (!File.Exists(csvPath))
{
    Console.WriteLine($"ERROR: File not found: {csvPath}");
    return;
}

string[] lines = File.ReadAllLines(csvPath);

if (lines.Length < 2)
{
    Console.WriteLine("ERROR: CSV file is empty or has no data.");
    return;
}

// Read CSV header
string[] headers = lines[0].Split(',');

Console.WriteLine("CSV Columns:");
foreach (string header in headers)
{
    Console.WriteLine($"- {header}");
}

Console.WriteLine();

// Parse CSV rows
List<Order> orders = new();

for (int i = 1; i < lines.Length; i++)
{
    string[] values = lines[i].Split(',');

    if (values.Length != 3)
    {
        Console.WriteLine($"WARNING: Invalid row {i + 1}");
        continue;
    }


    if (!int.TryParse(values[0], out int orderId) || orderId <= 0)
    {
        Console.WriteLine($"WARNING: Invalid OrderId on row {i + 1}");
        continue;
    }

    string customer = values[1].Trim();

    if (string.IsNullOrWhiteSpace(customer))
    {
        Console.WriteLine($"WARNING: Missing customer on row {i + 1}");
        continue;
    }

    if (!decimal.TryParse(values[2], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount) || amount < 0)
    {
        Console.WriteLine($"WARNING: Invalid amount on row {i + 1}");
        continue;
    }


    Order order = new()
    {
        OrderId = orderId,
        Customer = customer,
        Amount = amount
    };

    orders.Add(order);
    Console.WriteLine();
}

// Print parsed orders
Console.WriteLine("Parsed Orders:");

foreach (Order order in orders)
{
    Console.WriteLine(
        $"ID: {order.OrderId} | " +
        $"Customer: {order.Customer} | " +
        $"Amount: {order.Amount} CZK"
    );
}

Console.WriteLine();
Console.WriteLine($"Successfully parsed {orders.Count} orders.");
