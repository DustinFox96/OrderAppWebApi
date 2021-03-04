using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderAppWebApi.Models;

namespace OrderAppWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrderAppWebApi.Models.Customers> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Customers>(e => {
                e.HasIndex(c => c.Code).IsUnique(true);
            });
               
        }
    }
}
