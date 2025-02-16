using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessDescription",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "AccessDescription", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "190F2xxC-7177-4C77-BAd2-9121A40206BB", "Can Manipulate with Events, has access to own account manipulations", null, "Admin", "ADMIN" },
                    { "A117A8B5-F055-4A06-98A6-faxA4CEDBB24", "Can Subscribe/Unsubscribe on Events, has access to own account manipulations", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190F2xxC-7177-4C77-BAd2-9121A40206BB");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A117A8B5-F055-4A06-98A6-faxA4CEDBB24");

            migrationBuilder.DropColumn(
                name: "AccessDescription",
                table: "AspNetRoles");
        }
    }
}
