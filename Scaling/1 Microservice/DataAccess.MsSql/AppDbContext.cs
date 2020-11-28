using System;
using System.Collections.Generic;
using Entities.Enums;
using Entities.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.MsSql
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>().HasKey(x => new {x.OrderId, x.ProductId});

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product {Id = 1, Name = "Product 1", Price = 1, Weight = 1},
                new Product {Id = 2, Name = "Product 2", Price = 10, Weight = 10},
                new Product {Id = 3, Name = "Product 3", Price = 100, Weight = 100},
            });

            modelBuilder.Entity<Order>().HasData(new List<Order>
            {
                new Order {Id = 1, Status = OrderStatus.Created, CreateDate = new DateTime(2020, 11, 01)}
            });

            modelBuilder.Entity<OrderItem>().HasData(new List<OrderItem>
            {
                new OrderItem {OrderId = 1, ProductId = 1, Quantity = 1},
                new OrderItem {OrderId = 1, ProductId = 2, Quantity = 2},
            });
        }
    }
}
