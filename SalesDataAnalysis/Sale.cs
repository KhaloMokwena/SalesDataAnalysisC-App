using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDataAnalysis
{

    // Defining the Sale class to hold transaction data
    public class Sale
    {
        public string TransactionId { get; set; }
        public double Amount { get; set; }
        public string Product { get; set; }
        public int SalesPersonId { get; set; }
    }
}
