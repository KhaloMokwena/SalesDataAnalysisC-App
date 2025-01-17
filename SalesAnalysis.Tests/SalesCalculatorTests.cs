using SalesDataAnalysis;
using Xunit;
using System.Collections.Generic;

namespace SalesAnalysis.Tests
{
    public class SalesCalculatorTests
    {
        [Fact]
        public void GetTotalSalesAmount_ShouldReturnCorrectSum()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale { Amount = 100.50 },
                new Sale { Amount = 200.75 },
                new Sale { Amount = 150.00 }
            };

            // Act
            double result = SalesCalculator.GetTotalSalesAmount(sales);

            // Assert
            Assert.Equal(451.25, result, precision: 2);
        }

        [Fact]
        public void GetAverageSalesAmount_ShouldReturnCorrectAverage()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale { Amount = 100.50 },
                new Sale { Amount = 200.75 },
                new Sale { Amount = 150.00 }
            };

            // Act
            double result = SalesCalculator.GetAverageSalesAmount(sales);

            // Assert
            Assert.Equal(150.42, result, precision: 2);
        }

        [Fact]
        public void GetHighestSale_ShouldReturnCorrectMaxSale()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale { Amount = 100.50 },
                new Sale { Amount = 300.75 },
                new Sale { Amount = 150.00 }
            };

            // Act
            var result = SalesCalculator.GetHighestSale(sales);

            // Assert
            Assert.Equal(300.75, result.Amount);
        }

        [Fact]
        public void GetSalesByProduct_ShouldReturnCorrectSalesData()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale { Product = "Widget A", Amount = 100.50 },
                new Sale { Product = "Widget A", Amount = 150.00 },
                new Sale { Product = "Widget B", Amount = 200.75 }
            };

            // Act
            var result = SalesCalculator.GetSalesByProduct(sales);

            // Assert
            Assert.Equal(2, result["Widget A"].Total);
            Assert.Equal(125.25, result["Widget A"].Average, precision: 2);
            Assert.Equal(200.75, result["Widget B"].Total, precision: 2);
        }

        [Fact]
        public void GetSalesPersonWithMaxSales_ShouldReturnCorrectSalesPersonId()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale { SalesPersonId = 101, Amount = 100.50 },
                new Sale { SalesPersonId = 102, Amount = 300.00 },
                new Sale { SalesPersonId = 101, Amount = 150.00 }
            };

            // Act
            var result = SalesCalculator.GetSalesPersonWithMaxSales(sales);

            // Assert
            Assert.Equal(102, result);
        }

        [Fact]
        public void GetSalesCountBySalesPerson_ShouldReturnCorrectCounts()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale { SalesPersonId = 101 },
                new Sale { SalesPersonId = 102 },
                new Sale { SalesPersonId = 101 }
            };

            // Act
            var result = SalesCalculator.GetSalesCountBySalesPerson(sales);

            // Assert
            Assert.Equal(2, result[101]);
            Assert.Equal(1, result[102]);
        }
    }
}
