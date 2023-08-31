﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Products.Infrastructure.Persistence;

#nullable disable

namespace Products.Infrastructure.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20230808184518_ProductHistoryMigration")]
    partial class ProductHistoryMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Products.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PKWiU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PurchaseCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PurchaseGrossPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PurchaseNetPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("SaleCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SaleGrossPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("SaleNetPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("SplitPayment")
                        .HasColumnType("bit");

                    b.Property<decimal>("StockLevel")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("StockLevelControl")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VatFlag")
                        .HasColumnType("int");

                    b.Property<decimal>("VatValue")
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Products.Domain.Entities.ProductHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PKWiU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("PurchaseCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PurchaseGrossPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PurchaseNetPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("SaleCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SaleGrossPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("SaleNetPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("SplitPayment")
                        .HasColumnType("bit");

                    b.Property<decimal>("StockLevel")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("StockLevelControl")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VatFlag")
                        .HasColumnType("int");

                    b.Property<decimal>("VatValue")
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductHistories");
                });

            modelBuilder.Entity("Products.Domain.Entities.ProductHistory", b =>
                {
                    b.HasOne("Products.Domain.Entities.Product", "Product")
                        .WithMany("ProductHistories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Products.Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
