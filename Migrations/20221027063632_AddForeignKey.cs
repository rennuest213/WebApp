using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class AddForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Departments_DivisionID",
                table: "Departments",
                column: "DivisionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Divisions_DivisionID",
                table: "Departments",
                column: "DivisionID",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Divisions_DivisionID",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DivisionID",
                table: "Departments");
        }
    }
}
