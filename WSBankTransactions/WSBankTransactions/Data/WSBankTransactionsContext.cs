using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WSBankTransactions.Models
{
    public class WSBankTransactionsContext : DbContext
    {
        public WSBankTransactionsContext (DbContextOptions<WSBankTransactionsContext> options)
            : base(options)
        {
        }

        public DbSet<WSBankTransactions.Models.Transaction> Transaction { get; set; }
    }
}
