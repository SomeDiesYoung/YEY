using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class DefaultOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastOtpSentTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "6d5d60a9-fb02-47d9-ab18-09ffb3d139ad", "EventManagerOwner@yopmail.com", true, null, true, null, "EVENTMANAGEROWNER@YOPMAIL.COM", "SERVICEDEFAULTOWNER", "AQAAAAIAAYagAAAAEAAF5esazgEiCx4nV7td01kJLXjIW/ksY1LcTYuLvxsyW/gdxJOrNYRm/NA/AlhKNQ==", null, false, "c137550e-eb2d-4182-9c19-d98f612177ed", false, "ServiceDefaultOwner" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e2H52d72-326e-4AV3-8f1b-7d1a2c2ed14b", "8e445865-a24d-4543-a6c6-9443d048cdb9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e2H52d72-326e-4AV3-8f1b-7d1a2c2ed14b", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");
        }
    }
}
