﻿// <auto-generated />
using System;
using FinShark.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinShark.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241111194140_Sead Comment data")]
    partial class SeadCommentdata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinShark.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StockId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "This stock has shown consistent growth over the past year.",
                            CreatedOn = new DateTime(2024, 11, 1, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3315),
                            StockId = 1,
                            Title = "Great Stock!"
                        },
                        new
                        {
                            Id = 2,
                            Content = "Be cautious, as this stock tends to be very volatile.",
                            CreatedOn = new DateTime(2024, 11, 6, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3838),
                            StockId = 2,
                            Title = "High Volatility"
                        },
                        new
                        {
                            Id = 3,
                            Content = "Good for long-term investors who are looking for stable returns.",
                            CreatedOn = new DateTime(2024, 11, 8, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3842),
                            StockId = 1,
                            Title = "Solid Investment"
                        },
                        new
                        {
                            Id = 4,
                            Content = "I believe this stock is currently undervalued and could be a good buy.",
                            CreatedOn = new DateTime(2024, 11, 10, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3845),
                            StockId = 3,
                            Title = "Undervalued"
                        },
                        new
                        {
                            Id = 5,
                            Content = "This stock is trading at a high price-to-earnings ratio.",
                            CreatedOn = new DateTime(2024, 11, 11, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3848),
                            StockId = 4,
                            Title = "Overpriced"
                        });
                });

            modelBuilder.Entity("FinShark.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Industary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MarketCap")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Purchase")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("lastDiv")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Stocks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyName = "Tesla",
                            Industary = "Automotive",
                            MarketCap = 234234234L,
                            Purchase = 100m,
                            Symbol = "TSLA",
                            lastDiv = 2m
                        },
                        new
                        {
                            Id = 2,
                            CompanyName = "Microsoft",
                            Industary = "Technology",
                            MarketCap = 2342345L,
                            Purchase = 100m,
                            Symbol = "MSFT",
                            lastDiv = 1.2m
                        },
                        new
                        {
                            Id = 3,
                            CompanyName = "Vanguard Total Index",
                            Industary = "Index Fund",
                            MarketCap = 2342346L,
                            Purchase = 200m,
                            Symbol = "VTI",
                            lastDiv = 2.1m
                        },
                        new
                        {
                            Id = 4,
                            CompanyName = "Plantir",
                            Industary = "Technology",
                            MarketCap = 1234234L,
                            Purchase = 23m,
                            Symbol = "PLTR",
                            lastDiv = 0m
                        });
                });

            modelBuilder.Entity("FinShark.Models.Comment", b =>
                {
                    b.HasOne("FinShark.Models.Stock", "Stock")
                        .WithMany("Comments")
                        .HasForeignKey("StockId");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("FinShark.Models.Stock", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
