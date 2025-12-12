using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SureLbraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class ActualReturnDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 11, 24, 12, 39, 31, 61, DateTimeKind.Unspecified).AddTicks(2972), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 24, 12, 39, 31, 61, DateTimeKind.Unspecified).AddTicks(2974), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 11, 24, 12, 37, 20, 901, DateTimeKind.Unspecified).AddTicks(3692), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 24, 12, 37, 20, 901, DateTimeKind.Unspecified).AddTicks(3694), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
