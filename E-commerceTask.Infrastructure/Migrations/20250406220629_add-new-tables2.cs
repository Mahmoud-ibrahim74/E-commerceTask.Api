using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnewtables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "customer",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "email", "name" },
                values: new object[] { "mahmoud@example.com", "Mahmoud Ibrahim" });

            migrationBuilder.UpdateData(
                table: "customer",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "email", "name" },
                values: new object[] { "Ahmed@example.com", "Ahmed Ibrahim" });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "id",
                keyValue: 1,
                column: "order_date",
                value: new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "id",
                keyValue: 2,
                column: "order_date",
                value: new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "customer",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "email", "name" },
                values: new object[] { "john@example.com", "John Doe" });

            migrationBuilder.UpdateData(
                table: "customer",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "email", "name" },
                values: new object[] { "jane@example.com", "Jane Smith" });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "id",
                keyValue: 1,
                column: "order_date",
                value: new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "id",
                keyValue: 2,
                column: "order_date",
                value: new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
