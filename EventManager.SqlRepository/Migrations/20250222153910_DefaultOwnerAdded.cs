using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class DefaultOwnerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba088727-0278-45aa-81c6-3f82a1bff846", "AQAAAAIAAYagAAAAEGIOE+75U/gx0OdCzYPi19fYQZUZao7vshDU74orMUYLNSgWOuYq0uGUzi9IyKbATQ==", "7bf5fc7a-c12b-4b0c-a33d-f81969c277e9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d5d60a9-fb02-47d9-ab18-09ffb3d139ad", "AQAAAAIAAYagAAAAEAAF5esazgEiCx4nV7td01kJLXjIW/ksY1LcTYuLvxsyW/gdxJOrNYRm/NA/AlhKNQ==", "c137550e-eb2d-4182-9c19-d98f612177ed" });
        }
    }
}
