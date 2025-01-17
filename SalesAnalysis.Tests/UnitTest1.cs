using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Moq;
using SalesDataAnalysis;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace SalesAnalysis.Tests
{
    public class UnitTest1
    {
        private List<Sale> GetSampleSalesData()
        {
            return new List<Sale>
            {
                new Sale { TransactionId = "T001", Amount = 100.50, Product = "Widget A", SalesPersonId = 101 },
                new Sale { TransactionId = "T002", Amount = 200.75, Product = "Widget B", SalesPersonId = 102 },
                new Sale { TransactionId = "T003", Amount = 150.00, Product = "Widget A", SalesPersonId = 101 },
                new Sale { TransactionId = "T004", Amount = 300.00, Product = "Widget C", SalesPersonId = 103 },
                new Sale { TransactionId = "T005", Amount = 300.00, Product = "Widget C", SalesPersonId = 104 },
                new Sale { TransactionId = "T006", Amount = 200.75, Product = "Widget B", SalesPersonId = 104 }
            };
        }

        private string CaptureConsoleOutput(Action action)
        {
            var outputBuffer = new StringWriter();
            Console.SetOut(outputBuffer);
            action();
            return outputBuffer.ToString();
        }

        [Fact]
        public void ShouldCalculateTotalSales()
        {
            var salesData = GetSampleSalesData();

            double expectedTotalSales = 100.50 + 200.75 + 150.00 + 300.00 + 300.00 + 200.75;
            double actualTotalSales = Program.CalculateTotalSales(salesData);

            Assert.Equal(expectedTotalSales, actualTotalSales);
        }

        [Fact]
        public void ShouldDisplayTotalSalesMessage()
        {
            var mockInput = new Mock<TextReader>();
            mockInput.SetupSequence(reader => reader.ReadLine())
                     .Returns("1") // User selects option 1 for Total Sales
                     .Returns("7"); // User selects option 7 to exit
            Console.SetIn(mockInput.Object);

            string output = CaptureConsoleOutput(() => Program.Main(Array.Empty<string>()));

            Assert.Contains("Total Sales Amount: 1452.75", output);
        }

        [Fact]
        public void ShouldHandleInvalidOptionGracefully()
        {
            var mockInput = new Mock<TextReader>();
            mockInput.SetupSequence(reader => reader.ReadLine())
                     .Returns("999") // Invalid option
                     .Returns("7"); // User exits
            Console.SetIn(mockInput.Object);

            string output = CaptureConsoleOutput(() => Program.Main(Array.Empty<string>()));

            Assert.Contains("Invalid option. Please try again.", output);
        }

        [Fact]
        public void ShouldCalculateAverageSales()
        {
            var salesData = GetSampleSalesData();

            double expectedAverageSales = (100.50 + 200.75 + 150.00 + 300.00 + 300.00 + 200.75) / 6;
            double actualAverageSales = Program.CalculateAverageSales(salesData);

            Assert.Equal(expectedAverageSales, actualAverageSales);
        }

        [Fact]
        public void ShouldHandleEmptySalesData()
        {
            var salesData = new List<Sale>();

            double totalSales = Program.CalculateTotalSales(salesData);
            double averageSales = Program.CalculateAverageSales(salesData);
            double highestSale = Program.CalculateHighestSale(salesData);

            Assert.Equal(0, totalSales);
            Assert.Equal(0, averageSales);
            Assert.Equal(0, highestSale);
        }
    }
}
