using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rentos.Models;

namespace Rentos.Models
{
    public class RentosContext : DbContext
    {
        public RentosContext (DbContextOptions<RentosContext> options)
            : base(options)
        {
        }

        public DbSet<Rentos.Models.Car> Car { get; set; }

        public DbSet<Rentos.Models.Rent> Rent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
