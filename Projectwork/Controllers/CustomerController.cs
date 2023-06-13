using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
    }
}
