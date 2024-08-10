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
    public class PizzaTypeConfiguration : IEntityTypeConfiguration<PizzaType>
    {
        public void Configure(EntityTypeBuilder<PizzaType> builder)
        {
            builder.ToTable("pizza_types");

            builder.Property(pt => pt.PizzaTypeId).HasColumnName("pizza_type_id");
            builder.Property(pt => pt.Name).HasColumnName("name");
            builder.Property(pt => pt.Category).HasColumnName("category");
            builder.Property(pt => pt.Ingredients).HasColumnName("ingredients");

            builder.HasKey(pt => pt.PizzaTypeId);
            builder.HasMany(pt => pt.Pizzas)
                .WithOne(p => p.PizzaType)
                .HasForeignKey(p => p.PizzaTypeId);
        }

    }
}
