using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracker.Database.Migrations
{
    public partial class Added_relation_Owner_Assignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Assignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_OwnerId",
                table: "Assignment",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Owner_OwnerId",
                table: "Assignment",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Owner_OwnerId",
                table: "Assignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignment_OwnerId",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Assignment");
        }
    }
}
