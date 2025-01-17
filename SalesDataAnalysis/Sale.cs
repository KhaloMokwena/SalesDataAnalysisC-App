using System;

namespace SalesDataAnalysis
{
    /// <summary>
    /// Representing a single sales record from the CSV file.
    /// </summary>
    public class Sale
    {
        public string TransactionId { get; set; }

        public double Amount { get; set; }

        public string Product { get; set; }

        public int SalesPersonId { get; set; }

        // Overriding the ToString method to provide a string representation of the sale
        public override string ToString()
        {
            return $"TransactionId: {TransactionId}, Amount: {Amount}, Product: {Product}, SalesPersonId: {SalesPersonId}";
        }
    }
}

