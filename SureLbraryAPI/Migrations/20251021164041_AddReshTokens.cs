using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SureLbraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddReshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "RefreshToken", "RefreshTokenExpiryTime", "Role", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 10, 21, 16, 40, 40, 600, DateTimeKind.Unspecified).AddTicks(5262), new TimeSpan(0, 0, 0, 0, 0)), "Arinzechukwu", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTimeOffset(new DateTime(2025, 10, 21, 16, 40, 40, 600, DateTimeKind.Unspecified).AddTicks(5266), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "Role", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 10, 17, 15, 53, 48, 942, DateTimeKind.Unspecified).AddTicks(95), new TimeSpan(0, 0, 0, 0, 0)), "Admin", "", new DateTimeOffset(new DateTime(2025, 10, 17, 15, 53, 48, 942, DateTimeKind.Unspecified).AddTicks(96), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
