using System.ComponentModel.DataAnnotations;

namespace Projectwork.Models
{
    public class SupplyTransaction
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string SupplierName { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public int VideogameId { get; set; }
        public Videogame Videogame { get; set; }

        public SupplyTransaction() { }
    }
}
