using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projectwork.Models;
using Projectwork.Models.Transactions;
using System.Collections.Generic;

namespace Projectwork.Database
{
    public class ShopContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Videogame> Videogames { get; set; }
        public DbSet<SaleTransaction> Sales { get; set; }
        public DbSet<SupplyTransaction> Supplies { get; set; }
        public DbSet<StorageItem> StorageItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:videogamesdb.database.windows.net,1433;Initial Catalog=VideogamesDb;Persist Security Info=False;User ID=master;Password=ProjectWork3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
