using Projectwork.Models.Transactions;

namespace Projectwork.Models
{
    public class CashFlow
    {
        public List<Transaction> Transactions { get; set; }
        public List<SupplyTransaction> Supplies { get; set; }
        public List<SaleTransaction> Sales { get; set; }

        public CashFlow() 
        {
        }
    }
}
