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
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderProduct>OrdersProducts { get; set; }
        DbSet<OrderStatus> OrderStatus { get; set; }
        DbSet<OrderStatusUpdate> OrderStatusUpdate { get; set; }
        DbSet<Payment> Payments { get;set; }
        DbSet<Product> Products { get; set; }
        DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Review>().HasKey(x => new { x.ProductId, x.UserId });

            builder.Entity<Review>().HasOne(x => x.Product)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>().HasOne(x => x.User)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>().HasKey(x => new { x.CategoryId });
            builder.Entity<Product>().HasOne(x=>x.Category)
                  .WithMany(x=>x.Products)
                  .HasForeignKey(x=>x.CategoryId)
                  .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderProduct>().HasKey(x => new {x.ProductId,x.OrderId});
            builder.Entity<OrderProduct>().HasOne(x=>x.Order)
                .WithMany(x=>x.OrderProducts)
                .HasForeignKey(x=>x.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderProduct>().HasOne(x => x.Product)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Order>().HasKey(x => new { x.OrderStatusId });
            builder.Entity<Order>()
            .HasOne(o => o.OrderStatus)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.OrderStatusId);

            builder.Entity<OrderStatusUpdate>().HasKey(x => new { x.OrderId, x.OrderStatusId });
            builder.Entity<OrderStatusUpdate>()
                .HasOne(u => u.Order)
            .WithMany(o => o.StatusUpdates)
            .HasForeignKey(u => u.OrderId);

            builder.Entity<OrderStatusUpdate>()
                .HasOne(u => u.OrderStatus)
                .WithMany(s => s.OrderStatusUpdates)
                .HasForeignKey(u => u.OrderStatusId);


        }
    }
}
