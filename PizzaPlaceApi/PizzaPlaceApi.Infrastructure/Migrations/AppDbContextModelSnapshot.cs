﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaPlaceApi.Infrastructure.Data;

#nullable disable

namespace PizzaPlaceApi.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.6.24327.4");

            modelBuilder.Entity("PizzaPlaceApi.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("order_id");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT")
                        .HasColumnName("date");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("TEXT")
                        .HasColumnName("time");

                    b.HasKey("OrderId");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("PizzaPlaceApi.Domain.Entities.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("order_details_id");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("order_id");

                    b.Property<string>("PizzaId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("pizza_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("quantity");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PizzaId");

                    b.ToTable("order_details", (string)null);
                });

            modelBuilder.Entity("PizzaPlaceApi.Domain.Entities.Pizza", b =>
                {
                    b.Property<string>("PizzaId")
                        .HasColumnType("TEXT")
                        .HasColumnName("pizza_id");

                    b.Property<string>("PizzaTypeId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("pizza_type_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT")
                        .HasColumnName("price");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("size");

                    b.HasKey("PizzaId");

                    b.HasIndex("PizzaTypeId");

                    b.ToTable("pizzas", (string)null);
                });

            modelBuilder.Entity("PizzaPlaceApi.Domain.Entities.PizzaType", b =>
                {
                    b.Property<string>("PizzaTypeId")
                        .HasColumnType("TEXT")
                        .HasColumnName("pizza_type_id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("category");

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("ingredients");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("PizzaTypeId");

                    b.ToTable("pizza_types", (string)null);
                });

            modelBuilder.Entity("PizzaPlaceApi.Domain.Entities.OrderDetails", b =>
                {
                    b.HasOne("PizzaPlaceApi.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PizzaPlaceApi.Domain.Entities.Pizza", "Pizza")
                        .WithMany()
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("PizzaPlaceApi.Domain.Entities.Pizza", b =>
                {
                    b.HasOne("PizzaPlaceApi.Domain.Entities.PizzaType", "PizzaType")
                        .WithMany("Pizzas")
                        .HasForeignKey("PizzaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PizzaType");
                });

            modelBuilder.Entity("PizzaPlaceApi.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("PizzaPlaceApi.Domain.Entities.PizzaType", b =>
                {
                    b.Navigation("Pizzas");
                });
#pragma warning restore 612, 618
        }
    }
}
