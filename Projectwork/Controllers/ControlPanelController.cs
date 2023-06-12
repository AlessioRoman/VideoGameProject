using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projectwork.Database;
using Projectwork.Models;
using System.Data;

namespace Projectwork.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ControlPanelController : Controller
    {
        public IActionResult Inventory()
        {
            using (ShopContext db = new())
            {
                List<Videogame> videogame = db.Videogames.ToList();
                return View(videogame);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Videogame newVideogame)
        {
            if (!ModelState.IsValid)
            {
                return View(newVideogame);
            }
            else
            {
                using (ShopContext db = new())
                {
                    db.Videogames.Add(newVideogame);
                    db.SaveChanges();
                    return RedirectToAction("Inventory");
                }
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (ShopContext context = new())
            {
                Videogame? videogameToEdit = context.Videogames.Where(videogame => videogame.Id == id).FirstOrDefault();
                if (videogameToEdit == null)
                {
                    return NotFound("Il videogioco che cerchi non esiste!");
                }
                else
                {
                    return View("Update", videogameToEdit);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Videogame videogameUpdated)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", videogameUpdated);
            }

            using (ShopContext db = new())
            {
                Videogame? videogameToUpdate = db.Videogames.Where(videogame => videogame.Id == id).FirstOrDefault();
                if (videogameToUpdate == null)
                {
                    return NotFound();
                }
                else
                {
                    videogameToUpdate.Name = videogameUpdated.Name;
                    videogameToUpdate.ImgUrl = videogameUpdated.ImgUrl;
                    videogameToUpdate.Price = videogameUpdated.Price;
                    videogameToUpdate.Description = videogameUpdated.Description;
                    videogameToUpdate.Like = videogameUpdated.Like;
                    db.SaveChanges();
                    return RedirectToAction("Inventory");
                }
            }

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (ShopContext db = new())
            {
                Videogame? gameToDelete = db.Videogames.Where(videogame => videogame.Id == id).FirstOrDefault();
                if(gameToDelete != null)
                {
                    return View(gameToDelete);
                }
                return NotFound("Videogioco non trovato");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using (ShopContext db = new())
            {
                Videogame? gameToDelete = db.Videogames.Where(videogame => videogame.Id == id).FirstOrDefault();

                if (gameToDelete != null)
                {
                    db.Remove(gameToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Inventory");
                }
                return NotFound("Videogioco non trovato");
            }
        }

        [HttpGet]
        public IActionResult Supply()
        {
            using (ShopContext db = new())
            {
                List<Videogame> videogames = db.Videogames.ToList();
                SupplyTransactionForm model = new()
                {
                    Transaction = new SupplyTransaction(),
                    Videogames = videogames
                };

                return View("Supply", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Supply(SupplyTransactionForm data)
        {
            if (!ModelState.IsValid)
            {
                using (ShopContext db = new())
                {
                    List<Videogame> videogames = db.Videogames.ToList();
                    data.Videogames = videogames;

                    return View("Supply", data);
                }
            }

            using (ShopContext db = new())
            {
                SupplyTransaction newTransaction = new()
                {
                    SupplierName = data.Transaction.SupplierName,
                    VideogameId = data.Transaction.VideogameId,
                    Date = data.Transaction.Date,
                    Quantity = data.Transaction.Quantity,
                    Price = data.Transaction.Price
                };
                db.Supplies.Add(newTransaction);

                List<StorageItem> items = db.StorageItem.ToList();
                bool foundItem = false;
                foreach (StorageItem item in items)
                {
                    if  (newTransaction.VideogameId == item.VideogameId)
                    {
                        item.Quantity += newTransaction.Quantity;
                        foundItem = true;
                    }
                }
                if (!foundItem)
                {
                    StorageItem newItem = new()
                    {
                        Quantity = newTransaction.Quantity,
                        VideogameId = newTransaction.VideogameId
                    };
                    db.StorageItem.Add(newItem);
                }
                db.SaveChanges();
                return RedirectToAction("Storage");
            }
        }

        public IActionResult Sales() 
        {
            using (ShopContext db = new())
            {
                List<SaleTransaction> sales = db.Sales.ToList();
                return View(sales);
            }
        }


        public IActionResult Storage() 
        {
            using (ShopContext db = new())
            {
                List<StorageItem> items = db.StorageItem.ToList();
                foreach (StorageItem item in items)
                {
                    item.Videogame = db.Videogames.Where(v => v.Id == item.VideogameId).FirstOrDefault();
                }
                return View(items);
            }
        }
    }
}
