using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projectwork.Database;
using Projectwork.Models;

namespace Projectwork.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            using (ShopContext db = new())
            {
                Videogame[] mostLiked = new Videogame[5];
                mostLiked = db.Videogames.OrderByDescending(videogame => videogame.Like).Take(5).ToArray();

                return View(mostLiked);
            }
        }

        public IActionResult Catalogo()
        {
            using(ShopContext db = new())
            {
                List<Videogame> videogames = db.Videogames.ToList();
                return View(videogames);
            }
        }

        public IActionResult Details(int id)
        {
            using(ShopContext db = new())
            {
                Videogame? videogameDetail = db.Videogames.Where(videogame => videogame.Id == id).FirstOrDefault();
                if (videogameDetail != null)
                {
                    return View("Details", videogameDetail);
                } else
                {
                    return NotFound("Il gioco che stai cercando non esiste");
                }
            }
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
