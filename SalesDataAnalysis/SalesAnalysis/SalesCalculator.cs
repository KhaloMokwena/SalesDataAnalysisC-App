using System;
using System.Collections.Generic;
using System.Linq;
using SalesDataAnalysis;

namespace SalesAnalysis
{
    /// <summary>
    /// Contains business logic for analyzing sales data.
    /// </summary>
    public static class SalesCalculator
    {
        // Calculating the total sales amount
        public static double GetTotalSalesAmount(List<Sale> sales)
        {
            return sales.Sum(sale => sale.Amount);
        }

        // Calculating the average sales amount
        public static double GetAverageSalesAmount(List<Sale> sales)
        {
            return sales.Count > 0 ? sales.Average(sale => sale.Amount) : 0.0;
        }

        // Getting the highest sale
        public static Sale GetHighestSale(List<Sale> sales)
        {
            return sales.OrderByDescending(sale => sale.Amount).FirstOrDefault();
        }

        // Grouping sales by product and calculating total and average sales
        public static Dictionary<string, (double Total, double Average)> GetSalesByProduct(List<Sale> sales)
        {
            return sales.GroupBy(sale => sale.Product)
                        .ToDictionary(
                            group => group.Key,
                            group => (
                                Total: group.Sum(sale => sale.Amount),
                                Average: group.Average(sale => sale.Amount)
                            )
                        );
        }

        // Getting the salesperson with the maximum sales
        public static int GetSalesPersonWithMaxSales(List<Sale> sales)
        {
            return sales.GroupBy(sale => sale.SalesPersonId)
                        .OrderByDescending(group => group.Sum(sale => sale.Amount))
                        .FirstOrDefault()?.Key ?? -1;
        }

        // Counting the number of sales by each salesperson
        public static Dictionary<int, int> GetSalesCountBySalesPerson(List<Sale> sales)
        {
            return sales.GroupBy(sale => sale.SalesPersonId)
                        .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}
