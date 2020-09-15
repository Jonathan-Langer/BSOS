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
        public BSOSContext(DbContextOptions<BSOSContext> options):base(options)
        {

        }
        public BSOSContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //Create connections (M2M)
        {
            modelBuilder.Entity<ProductOrder>().HasKey(po => new { po.ProductId, po.OrderId });
            modelBuilder.Entity<ProductOrder>().HasOne(po => po.Product).WithMany(o => o.ProductOrders).HasForeignKey(po => po.ProductId);
            modelBuilder.Entity<ProductOrder>().HasOne(po => po.Order).WithMany(o => o.ProductOrders).HasForeignKey(po => po.OrderId);
        }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
    }
}
