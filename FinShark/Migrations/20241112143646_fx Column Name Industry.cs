using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinShark.Migrations
{
    /// <inheritdoc />
    public partial class fxColumnNameIndustry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Industary",
                table: "Stocks",
                newName: "Industry");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 2, 16, 36, 45, 485, DateTimeKind.Local).AddTicks(5212));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 7, 16, 36, 45, 485, DateTimeKind.Local).AddTicks(6370));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 9, 16, 36, 45, 485, DateTimeKind.Local).AddTicks(6379));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 11, 16, 36, 45, 485, DateTimeKind.Local).AddTicks(6386));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 12, 16, 36, 45, 485, DateTimeKind.Local).AddTicks(6392));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Industry",
                table: "Stocks",
                newName: "Industary");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 1, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3315));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 6, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3838));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 8, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3842));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 10, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3845));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 11, 21, 41, 39, 647, DateTimeKind.Local).AddTicks(3848));
        }
    }
}
