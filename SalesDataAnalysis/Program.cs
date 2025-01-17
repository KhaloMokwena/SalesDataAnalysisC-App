using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using SalesDataAnalysis; // This allows Program.cs to use the Sale class


[assembly: InternalsVisibleTo("SalesAnalysis.Tests")]  // Exposing internals to the test project

namespace SalesDataAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            // Loading sales data from the CSV file
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Data/sales_data.csv");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: CSV file not found at {Path.GetFullPath(filePath)}");
                Console.ReadLine();
                return;
            }

            var sales = LoadSalesData(filePath);
            if (sales == null)
            {
                return;
            }

            int option;
            do
            {
                // Displaying the menu options to the user
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Total sales amount");
                Console.WriteLine("2. Average sales amount per transaction");
                Console.WriteLine("3. Highest sale amount");
                Console.WriteLine("4. Total sales and average sales grouped by each product");
                Console.WriteLine("5. SalespersonId with maximum sales");
                Console.WriteLine("6. SalespersonID with number of sales done");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                // Reading user input and processing the selection
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine($"Total Sales Amount: {CalculateTotalSales(sales)}");
                            break;
                        case 2:
                            Console.WriteLine($"Average Sales Amount: {CalculateAverageSales(sales)}");
                            break;
                        case 3:
                            Console.WriteLine($"Highest Sale Amount: {CalculateHighestSale(sales)}");
                            break;
                        case 4:
                            DisplaySalesByProduct(sales);
                            break;
                        case 5:
                            Console.WriteLine($"SalespersonId with Maximum Sales: {GetSalesPersonWithMaxSales(sales)}");
                            break;
                        case 6:
                            GetSalesCountBySalesPerson(sales);
                            break;
                        case 7:
                            Console.WriteLine("Exiting the application.");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            } while (option != 7); // Repeat until the user chooses to exit
        }

        // Loading sales data from the CSV file
        private static List<Sale> LoadSalesData(string filePath)
        {
            return CsvHelper.ReadCsv(filePath, values =>
            {
                if (values.Length < 4)
                {
                    Console.WriteLine($"Invalid line format: {string.Join(";", values)}");
                    return null;
                }

                try
                {
                    return new Sale
                    {
                        TransactionId = values[0].Trim(),
                        Amount = double.Parse(values[1].Trim(), CultureInfo.InvariantCulture),
                        Product = values[2].Trim(),
                        SalesPersonId = int.Parse(values[3].Trim())
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {string.Join(";", values)}, Error: {ex.Message}");
                    return null;
                }
            }).Where(s => s != null).ToList();
        }

        public static double CalculateTotalSales(List<Sale> sales)
        {
            return sales.Sum(s => s.Amount);
        }

        public static double CalculateAverageSales(List<Sale> sales)
        {
            return sales.Count > 0 ? sales.Average(s => s.Amount) : 0;
        }

        public static double CalculateHighestSale(List<Sale> sales)
        {
            return sales.Count > 0 ? sales.Max(s => s.Amount) : 0;
        }

        // Displaying total and average sales grouped by each product
        private static void DisplaySalesByProduct(List<Sale> sales)
        {
            var groupedSales = sales.GroupBy(s => s.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalSales = g.Sum(s => s.Amount),
                    AverageSales = g.Average(s => s.Amount)
                });

            // Displaying the results
            Console.WriteLine("Sales grouped by product:");
            foreach (var group in groupedSales)
            {
                Console.WriteLine($"Product: {group.Product}, Total Sales: {group.TotalSales}, Average Sales: {group.AverageSales}");
            }
        }

        // Getting the SalespersonId with maximum sales
        private static int GetSalesPersonWithMaxSales(List<Sale> sales)
        {
            var maxSalesPerson = sales.GroupBy(s => s.SalesPersonId)
                .OrderByDescending(g => g.Sum(s => s.Amount))
                .FirstOrDefault();

            return maxSalesPerson?.Key ?? 0; // Returning 0 if no sales found
        }

        // Getting the count of sales done by each SalespersonID
        private static void GetSalesCountBySalesPerson(List<Sale> sales)
        {
            var salesCount = sales.GroupBy(s => s.SalesPersonId)
                .Select(g => new
                {
                    SalesPersonId = g.Key,
                    Count = g.Count()
                });

            // Displaying the results
            Console.WriteLine("Sales count by SalespersonID:");
            foreach (var count in salesCount)
            {
                Console.WriteLine($"SalespersonID: {count.SalesPersonId}, Count: {count.Count}");
            }
        }
    }
}