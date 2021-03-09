using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracker.Database.Migrations
{
    public partial class key_change_worker_job : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Name",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Job_Name",
                table: "Job");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Worker_Name_OwnerId",
                table: "Worker",
                columns: new[] { "Name", "OwnerId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Job_Name_OwnerId",
                table: "Job",
                columns: new[] { "Name", "OwnerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Worker_Name_OwnerId",
                table: "Worker");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Job_Name_OwnerId",
                table: "Job");

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
    }
}
