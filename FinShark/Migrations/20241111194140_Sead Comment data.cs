using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinShark.Migrations
{
    /// <inheritdoc />
    public partial class SeadCommentdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedOn", "StockId", "Title" },
                values: new object[,]
                {
                    { 1, "This stock has shown consistent growth over the past year.", new DateTime(2024, 11, 1, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3315), 1, "Great Stock!" },
                    { 2, "Be cautious, as this stock tends to be very volatile.", new DateTime(2024, 11, 6, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3838), 2, "High Volatility" },
                    { 3, "Good for long-term investors who are looking for stable returns.", new DateTime(2024, 11, 8, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3842), 1, "Solid Investment" },
                    { 4, "I believe this stock is currently undervalued and could be a good buy.", new DateTime(2024, 11, 10, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3845), 3, "Undervalued" },
                    { 5, "This stock is trading at a high price-to-earnings ratio.", new DateTime(2024, 11, 11, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3848), 4, "Overpriced" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
