
# CSV Automator

A lightweight C# console application for processing CSV order files and generating sales reports.

## Features

- Process multiple CSV files automatically
- Validate order IDs, customer names, and amounts
- Skip invalid records with warning messages
- Calculate total revenue and average order value
- Identify the largest order
- Export sales statistics to a CSV report

## Technologies

- C#
- .NET 10
- LINQ
- Git

## Project Structure

```text
CSV-Automator/
├── CsvAutomator/
│   ├── Data/
│   │   ├── Input/
│   │   │   ├── orders_january.csv
│   │   │   └── orders_february.csv
│   │   └── Output/
│   │       └── sales_report.csv
│   ├── Order.cs
│   ├── Program.cs
│   └── CsvAutomator.csproj
├── README.md
└── LICENSE
```

## How to Run

Requirements: .NET 10 SDK.

1. Clone this repository.
2. Open a terminal in the `CsvAutomator` directory.
3. Place CSV files inside `Data/Input`.
4. Run:

```bash
dotnet run
```

The application processes all `.csv` files in `Data/Input` and creates a report at `Data/Output/sales_report.csv`.

## Input Format

Each CSV file must contain a header and rows in this format:

```csv
OrderId,Customer,Amount
1001,Jan Novak,1500
1002,Petra Svobodova,2300
```

Rules:
- OrderId must be a positive integer.
- Customer must not be empty.
- Amount must be a non-negative number.
- Decimal values use a dot.

Note: The current parser uses simple comma splitting and does not support quoted CSV fields.

## Example Report

```csv
Metric,Value
TotalOrders,8
TotalRevenue,20790.00
AverageOrder,2598.75
LargestOrder,5100.00
LargestOrderCustomer,Karolina Vesela
LargestOrderId,1008
```

## Version

**0.4.0** – Multi-file processing, validation, sales statistics, and CSV report export.

## Roadmap

- Improved CSV parsing
- Duplicate order detection
- More detailed reports
- Command-line arguments
- Automated tests
