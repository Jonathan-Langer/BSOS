using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BSOS.Models;

namespace BSOS.Data
{
    public class BSOSContext : DbContext
    {
        public BSOSContext (DbContextOptions<BSOSContext> options)
            : base(options)
        {
        }

        public DbSet<BSOS.Models.Product> Product { get; set; }
    }
}
