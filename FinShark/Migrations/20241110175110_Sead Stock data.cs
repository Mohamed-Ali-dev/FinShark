using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinShark.Migrations
{
    /// <inheritdoc />
    public partial class SeadStockdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "CompanyName", "Industary", "MarketCap", "Purchase", "Symbol", "lastDiv" },
                values: new object[,]
                {
                    { 1, "Tesla", "Automotive", 234234234L, 100m, "TSLA", 2m },
                    { 2, "Microsoft", "Technology", 2342345L, 100m, "MSFT", 1.2m },
                    { 3, "Vanguard Total Index", "Index Fund", 2342346L, 200m, "VTI", 2.1m },
                    { 4, "Plantir", "Technology", 1234234L, 23m, "PLTR", 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
