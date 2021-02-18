using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracker.Database.Migrations
{
    public partial class AddedDefaultOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Id", "Email", "EncryptedPassword", "Name" },
                values: new object[] { 1, "admin@123", "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
