using Projectwork.Models.Transactions;

namespace Projectwork.Models
{
    public class SupplyTransactionForm
    {
        public SupplyTransaction Transaction { get; set; }
        public List<Videogame> Videogames { get; set; }

        public SupplyTransactionForm() { }
    }
}
