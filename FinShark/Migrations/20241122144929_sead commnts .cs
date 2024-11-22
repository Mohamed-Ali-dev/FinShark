using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinShark.Migrations
{
    /// <inheritdoc />
    public partial class seadcommnts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AppUserId", "Content", "CreatedOn", "StockId", "Title" },
                values: new object[,]
                {
                    { 1, "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4", "This stock has shown consistent growth over the past year.", new DateTime(2024, 11, 12, 16, 49, 27, 773, DateTimeKind.Local).AddTicks(5158), 1, "Great Stock!" },
                    { 2, "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4", "Be cautious, as this stock tends to be very volatile.", new DateTime(2024, 11, 17, 16, 49, 27, 773, DateTimeKind.Local).AddTicks(6345), 2, "High Volatility" },
                    { 3, "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4", "Good for long-term investors who are looking for stable returns.", new DateTime(2024, 11, 19, 16, 49, 27, 773, DateTimeKind.Local).AddTicks(6351), 1, "Solid Investment" },
                    { 4, "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4", "I believe this stock is currently undervalued and could be a good buy.", new DateTime(2024, 11, 21, 16, 49, 27, 773, DateTimeKind.Local).AddTicks(6355), 3, "Undervalued" },
                    { 5, "ab4d4d49-8f12-4afd-8e62-f6053c2af2b4", "This stock is trading at a high price-to-earnings ratio.", new DateTime(2024, 11, 22, 16, 49, 27, 773, DateTimeKind.Local).AddTicks(6359), 4, "Overpriced" }
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
