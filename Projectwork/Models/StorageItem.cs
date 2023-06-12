namespace Projectwork.Models
{
    public class StorageItem
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public int? VideogameId { get; set; }
        public Videogame Videogame { get; set; }

        public StorageItem()
        {

        }

    }
}
