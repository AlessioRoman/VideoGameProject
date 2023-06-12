using System.ComponentModel.DataAnnotations;

namespace Projectwork.Models
{
    public class Videogame
    {
        public int Id { get; set; }
        [MaxLength(400)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Url]
        public string ImgUrl { get; set; }
        public float Price { get; set; }
        public int Like { get; set; }

        public Videogame() { }
    }
}
