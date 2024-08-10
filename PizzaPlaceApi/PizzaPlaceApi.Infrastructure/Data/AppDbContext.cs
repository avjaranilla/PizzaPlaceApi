using Microsoft.EntityFrameworkCore;
using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaType> PizzaTypes { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and keys

            // Pizza
            modelBuilder.Entity<Pizza>()
                .HasKey(p => p.PizzaId);

            modelBuilder.Entity<Pizza>()
                .HasOne(p => p.PizzaType)
                .WithMany(pt => pt.Pizzas)
                .HasForeignKey(p => p.PizzaTypeId);

            // PizzaType
            modelBuilder.Entity<PizzaType>()
                .HasKey(pt => pt.PizzaTypeId);

            modelBuilder.Entity<PizzaType>()
                .HasMany(pt => pt.Pizzas)
                .WithOne(p => p.PizzaType)
                .HasForeignKey(p => p.PizzaTypeId);

            // Orders
            modelBuilder.Entity<Orders>()
                .HasKey(o => o.Id);

            // Update the relationship to match expected types
            modelBuilder.Entity<Orders>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .IsRequired();  // Make the foreign key required

            // OrderDetails
            modelBuilder.Entity<OrderDetails>()
                .HasKey(od => od.OrderDetailsId);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Pizza)
                .WithMany()
                .HasForeignKey(od => od.PizzaId);
        }


    }
}
