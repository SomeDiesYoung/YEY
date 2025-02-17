using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class OwnerRoleAddedInRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "AccessDescription", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e2H52d72-326e-4AV3-8f1b-7d1a2c2ed14b", "Can Manage Roles, can assign Roles to Users", "owner-concurrency-stamp", "Owner", "OWNER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2H52d72-326e-4AV3-8f1b-7d1a2c2ed14b");
        }
    }
}
