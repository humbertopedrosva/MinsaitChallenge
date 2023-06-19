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
    [Migration("20230619003636_MerchantRelease")]
    partial class MerchantRelease
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Minsait.Challenge.Domain.MerchantReleases.Entities.Release", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TypeRelease")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.ToTable("Release");
                });

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

            modelBuilder.Entity("Minsait.Challenge.Domain.MerchantReleases.Entities.Release", b =>
                {
                    b.HasOne("Minsait.Challenge.Domain.Merchants.Entities.Merchant", "Merchant")
                        .WithMany("Releases")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("Minsait.Challenge.Domain.Merchants.Entities.Merchant", b =>
                {
                    b.Navigation("Releases");
                });
#pragma warning restore 612, 618
        }
    }
}