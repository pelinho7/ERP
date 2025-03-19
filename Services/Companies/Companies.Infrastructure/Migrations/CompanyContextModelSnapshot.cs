﻿// <auto-generated />
using System;
using Companies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Companies.Infrastructure.Migrations
{
    [DbContext(typeof(CompanyContext))]
    partial class CompanyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Companies.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApartmentNo")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("Archived")
                        .HasColumnType("bit");

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasColumnType("varchar(34)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(120)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<int?>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("PostalOffice")
                        .IsRequired()
                        .HasColumnType("varchar(120)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("StreetNo")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SwiftCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VatId")
                        .IsRequired()
                        .HasColumnType("varchar(13)");

                    b.Property<string>("WwwAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("varchar(6)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });
#pragma warning restore 612, 618
        }
    }
}
