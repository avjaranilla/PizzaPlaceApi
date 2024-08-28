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
    public class PizzaConfiguration: IEntityTypeConfiguration<Pizza>
    {
        public void Configure(EntityTypeBuilder<Pizza> builder) 
        {
            builder.ToTable("pizzas");

            builder.Property(p => p.PizzaId).HasColumnName("pizza_id");
            builder.Property(p => p.PizzaTypeId).HasColumnName("pizza_type_id");
            builder.Property(p => p.Size).HasColumnName("size");
            builder.Property(p => p.Price).HasColumnName("price");

            builder.HasKey(p => p.PizzaId);
            builder.HasOne<PizzaType>()
                 .WithMany()  
                 .HasForeignKey(p => p.PizzaTypeId)
                 .IsRequired(false);  // Optional foreign key

        }
    }
}
