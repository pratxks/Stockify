﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stockify.Migrations.JobWorkEntryDb
{
    public partial class JobWorkEntryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JobWork Entries",
                columns: table => new
                {
                    JobWorkEntryId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobWorkId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrgId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", unicode: false, maxLength: 100, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", unicode: false, maxLength: 100, nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", unicode: false, maxLength: 100, nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", unicode: false, maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWork Entries", x => x.JobWorkEntryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobWork Entries");
        }
    }
}
