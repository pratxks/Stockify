﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stockify.Models;

#nullable disable

namespace Stockify.Migrations.StockDb
{
    [DbContext(typeof(StockDbContext))]
    [Migration("20230426130439_StockMigration")]
    partial class StockMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Stockify.Models.Stock", b =>
                {
                    b.Property<string>("StockId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("StockId");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Height")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LoadGroup")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("OrgId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal?>("Quantity")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Weight")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Width")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("StockId");

                    b.ToTable("Stocks", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}