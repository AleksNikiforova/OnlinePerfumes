using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.DataAccess
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        DbSet<Customer> Customer { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderProduct>OrdersProducts { get; set; }
        DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

          //   builder.Entity<Category>().HasData(
          //    new Category { Id = 1, Name = "Парфюми за жени" },
          //    new Category { Id = 2, Name = "Парфюми за мъже" },
          //    new Category { Id = 3, Name = "Унисекс парфюми" },
          //    new Category { Id = 4, Name = "Луксозни парфюми" }
          //);

           /*  builder.Entity<Product>().HasData(
             new Product { ProductId = 1, Name = "Chanel No.5", Price = 120, CategoryId = 1, Aroma = "Цветен" },
             new Product { ProductId = 2, Name = "Dior", Price = 95, CategoryId = 2, Aroma = "Дървесен" },
             new Product { ProductId = 3, Name = "Opium", Price = 100, CategoryId = 3, Aroma = "Цветен с ванилия" }
      );*/
            

            //builder.Entity<Product>().HasKey(x => new { x.CategoryId });
            builder.Entity<Product>().HasOne(x=>x.Category)
                  .WithMany(x=>x.Products)
                  .HasForeignKey(x=>x.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderProduct>().HasKey(x => new {x.ProductId,x.OrderId});
            builder.Entity<OrderProduct>().HasOne(x=>x.Order)
                .WithMany(x=>x.OrderProducts)
                .HasForeignKey(x=>x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderProduct>().HasOne(x => x.Product)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<CartItem>()
                .HasOne(ci => ci.Customer)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            /* builder.Entity<Order>()
             .HasOne(o => o.Customer)
             .WithMany(c => c.Orders)
             .HasForeignKey(o => o.CustomerId);*/
            base.OnModelCreating(builder);




        }
    }
}
