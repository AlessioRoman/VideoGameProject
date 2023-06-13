namespace Projectwork.Models.Transactions
{
    public class SaleTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }

        public int VideogameId { get; set; }
        public Videogame Videogame { get; set; }

        public SaleTransaction() { }
    }
}
