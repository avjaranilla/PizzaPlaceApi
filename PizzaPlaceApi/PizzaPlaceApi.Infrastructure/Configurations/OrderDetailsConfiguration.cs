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
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("order_details");

            builder.Property(od => od.OrderDetailsId).HasColumnName("order_details_id");
            builder.Property(od => od.OrderId).HasColumnName("order_id");
            builder.Property(od => od.PizzaId).HasColumnName("pizza_id");
            builder.Property(od => od.Quantity).HasColumnName("quantity");

            builder.HasKey(od => od.OrderDetailsId);

            builder.HasOne<Order>()  
                .WithMany()  
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);  // Keep the cascade delete behavior

            builder.HasOne<Pizza>()  
                .WithMany()  
                .HasForeignKey(od => od.PizzaId);
        }
    }
}
