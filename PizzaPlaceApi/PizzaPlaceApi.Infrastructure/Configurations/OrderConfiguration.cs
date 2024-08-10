using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.Property(o => o.OrderId).HasColumnName("order_id");
            builder.Property(o => o.Date).HasColumnName("date");
            builder.Property(o => o.Time).HasColumnName("time");

            builder.HasKey(o => o.OrderId);

            // Update the relationship to match expected types
            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .IsRequired();  // Make the foreign key required
        }
    }
}
