using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Spendings",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2024, 9, 30, 20, 49, 7, 446, DateTimeKind.Local).AddTicks(9026));

            migrationBuilder.UpdateData(
                table: "Spendings",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2024, 9, 30, 20, 49, 7, 446, DateTimeKind.Local).AddTicks(9078));

            migrationBuilder.UpdateData(
                table: "Spendings",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2024, 9, 30, 20, 49, 7, 446, DateTimeKind.Local).AddTicks(9081));

            migrationBuilder.CreateIndex(
                name: "IX_Spendings_CategoryId",
                table: "Spendings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spendings_Categories_CategoryId",
                table: "Spendings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spendings_Categories_CategoryId",
                table: "Spendings");

            migrationBuilder.DropIndex(
                name: "IX_Spendings_CategoryId",
                table: "Spendings");

            migrationBuilder.UpdateData(
                table: "Spendings",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2024, 9, 27, 15, 26, 9, 584, DateTimeKind.Local).AddTicks(706));

            migrationBuilder.UpdateData(
                table: "Spendings",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2024, 9, 27, 15, 26, 9, 584, DateTimeKind.Local).AddTicks(745));

            migrationBuilder.UpdateData(
                table: "Spendings",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2024, 9, 27, 15, 26, 9, 584, DateTimeKind.Local).AddTicks(748));
        }
    }
}
