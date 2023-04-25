﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stockify.Models;

#nullable disable

namespace Stockify.Migrations.JobWorkEntryDb
{
    [DbContext(typeof(JobWorkEntryDbContext))]
    [Migration("20230425123006_JobWorkEntryMigration")]
    partial class JobWorkEntryMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Stockify.Models.JobWorkEntry", b =>
                {
                    b.Property<string>("JobWorkEntryId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("JobWorkEntryId");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Height")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("JobWorkId")
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

                    b.HasKey("JobWorkEntryId");

                    b.ToTable("JobWork Entries", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
