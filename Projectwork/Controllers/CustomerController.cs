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

        public IActionResult Acquista(int id) 
        { 
            using (ShopContext db = new())
            {
                SaleTransactionForm data = new();
                data.Videogame = db.Videogames.Where(v => v.Id == id).FirstOrDefault();

                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Acquista(SaleTransactionForm data, int id)
        {
            if (!ModelState.IsValid)
            {
                using (ShopContext db = new())
                {
                    return View("Supply", data);
                }
            }

            using (ShopContext db = new())
            {
                data.Videogame = db.Videogames.Where(v => v.Id == id).FirstOrDefault();

                SaleTransaction newTransaction = new()
                {
                    Date = DateTime.Now,
                    Quantity = data.Transaction.Quantity,
                    VideogameId = data.Videogame.Id,
                    Videogame = data.Videogame
                };
                db.Sales.Add(newTransaction);
                db.SaveChanges();

                return RedirectToAction("Catalogo");
            }
        }

        public IActionResult Like()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Like(int id)
        {
            using (ShopContext db = new())
            {
                Videogame gameToLike = db.Videogames.Where(v => v.Id == id).FirstOrDefault();
                gameToLike.Like += 1;
                db.SaveChanges();

                return RedirectToAction("Catalogo");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
