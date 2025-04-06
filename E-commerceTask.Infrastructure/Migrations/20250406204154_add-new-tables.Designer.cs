﻿// <auto-generated />
using System;
using E_commerceTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_commerceTask.Infrastructure.Migrations
{
    [DbContext(typeof(ECommerceTaskContext))]
    [Migration("20250406204154_add-new-tables")]
    partial class addnewtables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_commerceTask.Domain.Models.Customers.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("customer");

                    b.HasData(
                        new
                        {
                            id = 1,
                            email = "john@example.com",
                            name = "John Doe",
                            phone = "1234567890"
                        },
                        new
                        {
                            id = 2,
                            email = "jane@example.com",
                            name = "Jane Smith",
                            phone = "0987654321"
                        });
                });

            modelBuilder.Entity("E_commerceTask.Domain.Models.Orders.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("order_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("total_price")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("customer_id");

                    b.ToTable("order");

                    b.HasData(
                        new
                        {
                            id = 1,
                            customer_id = 1,
                            order_date = new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Local),
                            status = "Pending",
                            total_price = 1570.0
                        },
                        new
                        {
                            id = 2,
                            customer_id = 2,
                            order_date = new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Local),
                            status = "Delivered",
                            total_price = 95.0
                        });
                });

            modelBuilder.Entity("E_commerceTask.Domain.Models.Orders.OrderProduct", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("order_id");

                    b.HasIndex("product_id");

                    b.ToTable("order_product");

                    b.HasData(
                        new
                        {
                            id = 1,
                            order_id = 1,
                            product_id = 1
                        },
                        new
                        {
                            id = 2,
                            order_id = 1,
                            product_id = 3
                        },
                        new
                        {
                            id = 3,
                            order_id = 2,
                            product_id = 2
                        },
                        new
                        {
                            id = 4,
                            order_id = 2,
                            product_id = 3
                        });
                });

            modelBuilder.Entity("E_commerceTask.Domain.Models.Products.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("product");

                    b.HasData(
                        new
                        {
                            id = 1,
                            description = "High-end gaming laptop",
                            name = "Laptop",
                            price = 1500.0,
                            stock = 10
                        },
                        new
                        {
                            id = 2,
                            description = "Wireless mouse",
                            name = "Mouse",
                            price = 25.0,
                            stock = 100
                        },
                        new
                        {
                            id = 3,
                            description = "Mechanical keyboard",
                            name = "Keyboard",
                            price = 70.0,
                            stock = 50
                        });
                });

            modelBuilder.Entity("E_commerceTask.Domain.Models.Orders.Order", b =>
                {
                    b.HasOne("E_commerceTask.Domain.Models.Customers.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("E_commerceTask.Domain.Models.Orders.OrderProduct", b =>
                {
                    b.HasOne("E_commerceTask.Domain.Models.Orders.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_commerceTask.Domain.Models.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("E_commerceTask.Domain.Models.Orders.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
