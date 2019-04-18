﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Data.EntityFrameworkCore;

namespace Store.Data.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(EfCoreApplicationDbContext))]
    [Migration("20190418163806_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Store.Domain.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2019, 4, 18, 21, 8, 4, 795, DateTimeKind.Local).AddTicks(7267),
                            IsRemoved = false,
                            Title = "iPhone 7"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2019, 4, 18, 21, 8, 4, 805, DateTimeKind.Local).AddTicks(8178),
                            IsRemoved = false,
                            Title = "Samsong A9"
                        });
                });

            modelBuilder.Entity("Store.Domain.Entity.ProductSku", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSku");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2019, 4, 18, 21, 8, 4, 810, DateTimeKind.Local).AddTicks(5222),
                            IsRemoved = false,
                            Price = 899m,
                            ProductId = 1,
                            Quantity = 10,
                            Title = "64G"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2019, 4, 18, 21, 8, 4, 811, DateTimeKind.Local).AddTicks(4508),
                            IsRemoved = false,
                            Price = 990m,
                            ProductId = 1,
                            Quantity = 5,
                            Title = "128G"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2019, 4, 18, 21, 8, 4, 811, DateTimeKind.Local).AddTicks(4703),
                            IsRemoved = false,
                            Price = 750m,
                            ProductId = 2,
                            Quantity = 20,
                            Title = "256G"
                        });
                });

            modelBuilder.Entity("Store.Domain.Entity.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<int>("ProductSkuId");

                    b.Property<int>("Quantity");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ProductSkuId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("Store.Domain.Entity.ProductSku", b =>
                {
                    b.HasOne("Store.Domain.Entity.Product", "Product")
                        .WithMany("ProductSkus")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Store.Domain.Entity.Purchase", b =>
                {
                    b.HasOne("Store.Domain.Entity.ProductSku", "ProductSku")
                        .WithMany()
                        .HasForeignKey("ProductSkuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}