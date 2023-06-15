using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projectwork.Database;
using Projectwork.Models;
using System.Diagnostics;
using System.Security.Cryptography.Xml;

namespace Projectwork.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (ShopContext db = new())
            {
                CashFlow newCashflow = new();
                newCashflow.Supplies = db.Supplies.ToList();
                newCashflow.Sales = db.Sales.ToList();
                newCashflow.Transactions = new();

                foreach (SaleTransaction sale in newCashflow.Sales)
                {
                    sale.Videogame = db.Videogames.Where(v => v.Id == sale.VideogameId).FirstOrDefault();
                    Transaction newTransaction = new(sale.Date, sale.Quantity, sale.Videogame.Price, "Entrata", sale.VideogameId, sale.Videogame);
                    newCashflow.Transactions.Add(newTransaction);
                }
                foreach (SupplyTransaction order in newCashflow.Supplies)
                {
                    order.Videogame = db.Videogames.Where(v => v.Id == order.VideogameId).FirstOrDefault();
                    Transaction newTransaction = new(order.Date, order.Quantity, order.Price, "Uscita", order.VideogameId, order.Videogame);
                    newCashflow.Transactions.Add(newTransaction);
                }
                newCashflow.Transactions = newCashflow.Transactions.OrderByDescending(transaction => transaction.Date).ToList();

                return View(newCashflow);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}