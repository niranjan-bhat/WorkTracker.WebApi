using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracker.Database.Migrations
{
    public partial class JobName_made_unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Worker",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Job",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Name",
                table: "Worker",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_Name",
                table: "Job",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Name",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Job_Name",
                table: "Job");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
