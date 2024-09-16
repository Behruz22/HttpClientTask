using HttpClientTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTask.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<OutgoingModel> CurrencyExchangeRates { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = "Server = (localdb)\\MSSQLLocalDB; " +
                "Database = ExchangeRateDB;" +
                "Trusted_Connection = True;";

            optionsBuilder.UseSqlServer(path);
        }
    }
}
