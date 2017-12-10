using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSBankTransactions.Models
{
    public class Transaction
    {
        public decimal ID { get; set; }
        public int Step { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string NameOrig { get; set; }
        public decimal OldbalanceOrig { get; set; }
        public decimal NewbalanceOrig { get; set; }
        public string NameDest { get; set; }
        public decimal OldbalanceDest { get; set; }
        public decimal NewbalanceDest { get; set; }
        public bool IsFraud { get; set; }
        public bool IsFlaggedFraud { get; set; }
    }
}
