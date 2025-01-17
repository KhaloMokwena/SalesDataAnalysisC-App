using SalesDataAnalysis.Data;
using SalesAnalysis;
//using static SalesAnalysis.Tests.ProgramTests;
using System.Globalization;

namespace SalesDataAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            // Defining the file path to the sales data CSV file
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Data/sales_data.csv");
            List<Sale> salesData = null;

            try
            {
                // Reading the sales data from the CSV file
                salesData = CsvHelper.ReadCsv(filePath, MapSale);
            }
            catch (Exception ex)
            {
                // Handling any errors that occur during file reading
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }

            var menuOption = 0;
            while (menuOption != 7)
            {
                // Displaying the menu options to the user
                DisplayMenu();
                if (!int.TryParse(Console.ReadLine(), out menuOption) || menuOption < 1 || menuOption > 7)
                {
                    // Handling invalid menu selections
                    Console.WriteLine("Invalid selection. Please choose a valid option.");
                    continue;
                }

                // Executing the selected menu option
                switch (menuOption)
                {
                    case 1:
                        DisplayTotalSales(salesData);
                        break;
                    case 2:
                        DisplayAverageSales(salesData);
                        break;
                    case 3:
                        DisplayHighestSale(salesData);
                        break;
                    case 4:
                        DisplaySalesGroupedByProduct(salesData);
                        break;
                    case 5:
                        DisplaySalespersonWithMaxSales(salesData);
                        break;
                    case 6:
                        DisplaySalespersonWithSalesCount(salesData);
                        break;
                    case 7:
                        Console.WriteLine("Exiting the application...");
                        break;
                }
            }
        }

        // Displaying the menu options to the user
        static void DisplayMenu()
        {
            Console.WriteLine("1. Total sales amount");
            Console.WriteLine("2. Average sales amount per transaction");
            Console.WriteLine("3. Highest sale amount");
            Console.WriteLine("4. Total sales and average sales grouped by each product");
            Console.WriteLine("5. SalespersonId with maximum sales");
            Console.WriteLine("6. SalespersonId with the number of sales done");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option: ");
        }

        // Displaying the total sales amount
        static void DisplayTotalSales(List<Sale> salesData)
        {
            if (salesData == null || !salesData.Any())
            {
                Console.WriteLine("No sales data available to calculate the Total Sales.");
                return;
            }

            var totalSales = salesData.Sum(s => s.Amount);
            Console.WriteLine($"Total Sales: {totalSales:C2}");
        }

        // Displaying the average sales amount per transaction
        static void DisplayAverageSales(List<Sale> salesData)
        {
            if (salesData == null || !salesData.Any())
            {
                Console.WriteLine("No sales data available to calculate the average.");
                return;
            }

            var averageSales = salesData.Average(s => s.Amount);
            Console.WriteLine($"Average Sales: {averageSales:C2}");
        }

        // Displaying the highest sale amount
        static void DisplayHighestSale(List<Sale> salesData)
        {
            if (salesData == null || !salesData.Any())
            {
                Console.WriteLine("No sales data available.");
                return;
            }

            var highestSale = salesData.Max(s => s.Amount);
            Console.WriteLine($"Highest Sale: {highestSale:C2}");
        }

        // Displaying total sales and average sales grouped by each product
        static void DisplaySalesGroupedByProduct(List<Sale> salesData)
        {
            var groupedSales = salesData
                .GroupBy(s => s.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalSales = g.Sum(s => s.Amount),
                    AverageSales = g.Average(s => s.Amount)
                });

            foreach (var group in groupedSales)
            {
                Console.WriteLine($"{group.Product} - Total Sales: {group.TotalSales:C2}, Average Sales: {group.AverageSales:C2}");
            }
        }

        // Displaying the salesperson with the highest total sales
        static void DisplaySalespersonWithMaxSales(List<Sale> salesData)
        {
            try
            {
                if (salesData == null || !salesData.Any())
                {
                    Console.WriteLine("No sales data available.");
                    return;
                }

                var salesperson = salesData
                    .GroupBy(s => s.SalesPersonId)
                    .Select(g => new
                    {
                        SalesPersonId = g.Key,
                        TotalSales = g.Sum(s => s.Amount)
                    })
                    .OrderByDescending(g => g.TotalSales)
                    .First();

                Console.WriteLine($"Salesperson {salesperson.SalesPersonId} had the highest total sales: {salesperson.TotalSales:C2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying the salesperson with the highest sales: {ex.Message}");
            }
        }

        // Displaying the salesperson with the most sales transactions
        static void DisplaySalespersonWithSalesCount(List<Sale> salesData)
        {
            try
            {
                if (salesData == null || !salesData.Any())
                {
                    Console.WriteLine("No sales data available.");
                    return;
                }

                var salesperson = salesData
                    .GroupBy(s => s.SalesPersonId)
                    .Select(g => new
                    {
                        SalesPersonId = g.Key,
                        SalesCount = g.Count()
                    })
                    .OrderByDescending(g => g.SalesCount)
                    .First();

                Console.WriteLine($"Salesperson {salesperson.SalesPersonId} made the most sales: {salesperson.SalesCount} transactions");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying the salesperson with the most sales: {ex.Message}");
            }
        }

        // Mapping the CSV data to the Sale object
        static Sale MapSale(string[] values)
        {
            return new Sale
            {
                TransactionId = values[0],
                Amount = (double)decimal.Parse(values[1], CultureInfo.InvariantCulture),
                Product = values[2],
                SalesPersonId = int.Parse(values[3])
            };
        }
    }
}
