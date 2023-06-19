﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Minsait.Challenge.Infra;

#nullable disable

namespace Minsait.Challenge.Infra.Migrations
{
    [DbContext(typeof(MerchantContext))]
    [Migration("20230618230505_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Minsait.Challenge.Domain.Merchants.Entities.Merchant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Merchant");

                    b.HasData(
                        new
                        {
                            Id = new Guid("661b8028-6ce0-4544-950d-18837c2bcd7e"),
                            Email = "admin@admin.com",
                            Name = "Admin",
                            PasswordHash = "18a948b42a6f1fa8b84bfc73c8a967b1df15ee4dbd08e9bd150441b5e576698c",
                            Surname = ""
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
