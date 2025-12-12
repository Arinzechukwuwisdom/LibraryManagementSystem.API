using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SureLbraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class ReturnDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualReturnDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 11, 24, 12, 37, 20, 901, DateTimeKind.Unspecified).AddTicks(3692), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 24, 12, 37, 20, 901, DateTimeKind.Unspecified).AddTicks(3694), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualReturnDate",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 10, 21, 16, 40, 40, 600, DateTimeKind.Unspecified).AddTicks(5262), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 10, 21, 16, 40, 40, 600, DateTimeKind.Unspecified).AddTicks(5266), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
