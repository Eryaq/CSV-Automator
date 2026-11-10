# CSV Automator

A C# .NET 10 application designed to automate repetitive CSV data processing tasks.

## Project Goal

The goal of CSV Automator is to help businesses save time by automating CSV file merging, validation, and export.

## Planned Features
- [x] Load CSV data from a local file
- [x] Parse CSV headers and order records
- [x] Validate order IDs, customer names, and amounts
- [x] Skip invalid rows with warning messages
- [ ] Merge multiple CSV files
- [ ] Export processed data into a new CSV file
- [ ] Add a graphical user interface

## Tech Stack

- C#
- .NET 10
- Visual Studio Code
- Git and GitHub

## Current Status

Version 0.3.0 - Basic CSV loading, parsing, and validation implemented.

The application currently supports a simple comma-separated order format with three columns: OrderId, Customer, and Amount.

Quoted CSV fields, advanced validation, and merging multiple files are not yet supported.

## Getting Started

Install the .NET 10 SDK, then run:

```bash
dotnet run --project CsvAutomator/CsvAutomator.csproj
```

## License

MIT License.
