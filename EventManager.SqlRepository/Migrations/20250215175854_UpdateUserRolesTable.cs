using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190F2xxC-7177-4C77-BAd2-9121A40206BB",
                column: "ConcurrencyStamp",
                value: "2a43330a-c971-47ff-b2ef-47a621c62939");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A117A8B5-F055-4A06-98A6-faxA4CEDBB24",
                column: "ConcurrencyStamp",
                value: "07e50ece-7018-4818-a582-cfce45465e1a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190F2xxC-7177-4C77-BAd2-9121A40206BB",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A117A8B5-F055-4A06-98A6-faxA4CEDBB24",
                column: "ConcurrencyStamp",
                value: null);
        }
    }
}
