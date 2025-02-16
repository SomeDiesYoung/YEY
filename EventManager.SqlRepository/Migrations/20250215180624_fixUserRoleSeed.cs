using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class fixUserRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190F2xxC-7177-4C77-BAd2-9121A40206BB",
                column: "ConcurrencyStamp",
                value: "admin-concurrency-stamp");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A117A8B5-F055-4A06-98A6-faxA4CEDBB24",
                column: "ConcurrencyStamp",
                value: "member-concurrency-stamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190F2xxC-7177-4C77-BAd2-9121A40206BB",
                column: "ConcurrencyStamp",
                value: "6b41daeb-d815-4e33-a846-e81fa8f38da0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A117A8B5-F055-4A06-98A6-faxA4CEDBB24",
                column: "ConcurrencyStamp",
                value: "3697cb8f-f2bc-42e9-a0e6-f22d4a75922c");
        }
    }
}
