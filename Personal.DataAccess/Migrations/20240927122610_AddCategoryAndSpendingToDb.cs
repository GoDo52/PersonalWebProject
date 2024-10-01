using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Personal.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndSpendingToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spendings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spendings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Food" },
                    { 2, "Convenience" },
                    { 3, "Medicine" },
                    { 4, "Restaurant" },
                    { 5, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Spendings",
                columns: new[] { "Id", "Amount", "CategoryId", "DateTime", "Description", "UserId" },
                values: new object[,]
                {
                    { 1, 10m, 1, new DateTime(2024, 9, 27, 15, 26, 9, 584, DateTimeKind.Local).AddTicks(706), null, 1 },
                    { 2, 19m, 4, new DateTime(2024, 9, 27, 15, 26, 9, 584, DateTimeKind.Local).AddTicks(745), null, 2 },
                    { 3, 5m, 2, new DateTime(2024, 9, 27, 15, 26, 9, 584, DateTimeKind.Local).AddTicks(748), null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Spendings");
        }
    }
}
