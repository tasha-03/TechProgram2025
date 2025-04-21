﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechProgram2025.Migrations
{
    /// <inheritdoc />
    public partial class Added_Contract_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contracts");
        }
    }
}
