using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracker.Database.Migrations
{
    public partial class workerMobileUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "Worker",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Mobile",
                table: "Worker",
                column: "Mobile",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Mobile",
                table: "Worker");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
