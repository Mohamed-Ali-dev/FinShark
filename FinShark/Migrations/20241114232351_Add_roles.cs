using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinShark.Migrations
{
    /// <inheritdoc />
    public partial class Add_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f0e8226-8cb6-40be-b00a-230436f31a95", "b6bccc12-b0b9-4f3d-be01-9aed4e36b727", "User", "USER" },
                    { "83803995-f3b6-4e7f-90dc-893fc0ed704b", "b93f27ec-8261-48bb-aa9c-39ec5b5c3f3e", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 5, 1, 23, 51, 7, DateTimeKind.Local).AddTicks(6102));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 10, 1, 23, 51, 7, DateTimeKind.Local).AddTicks(6644));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 12, 1, 23, 51, 7, DateTimeKind.Local).AddTicks(6648));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 14, 1, 23, 51, 7, DateTimeKind.Local).AddTicks(6651));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 15, 1, 23, 51, 7, DateTimeKind.Local).AddTicks(6654));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f0e8226-8cb6-40be-b00a-230436f31a95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83803995-f3b6-4e7f-90dc-893fc0ed704b");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 4, 18, 1, 49, 228, DateTimeKind.Local).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 9, 18, 1, 49, 228, DateTimeKind.Local).AddTicks(8465));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 11, 18, 1, 49, 228, DateTimeKind.Local).AddTicks(8473));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 13, 18, 1, 49, 228, DateTimeKind.Local).AddTicks(8479));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 14, 18, 1, 49, 228, DateTimeKind.Local).AddTicks(8486));
        }
    }
}
