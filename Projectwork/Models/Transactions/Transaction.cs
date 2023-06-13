namespace Projectwork.Models.Transactions
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }

        public int? VideogameId { get; set; }
        public Videogame Videogame { get; set; }

        public Transaction() { }

        public Transaction(DateTime date, int quantity, float price, string type, int videogameId, Videogame videogame)
        {
            Date = date;
            Quantity = quantity;
            Price = price;
            Type = type;
            VideogameId = videogameId;
            Videogame = videogame;
        }
    }
}
