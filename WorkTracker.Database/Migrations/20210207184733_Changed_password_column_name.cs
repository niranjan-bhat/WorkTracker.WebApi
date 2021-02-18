using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracker.Database.Migrations
{
    public partial class Changed_password_column_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Owner",
                newName: "EncryptedPassword");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EncryptedPassword",
                table: "Owner",
                newName: "Password");
        }
    }
}
