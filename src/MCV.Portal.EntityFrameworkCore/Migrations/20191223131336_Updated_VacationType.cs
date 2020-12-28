using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class Updated_VacationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VacationTypes_CreatorUserId",
                table: "VacationTypes",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationTypes_AbpUsers_CreatorUserId",
                table: "VacationTypes",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationTypes_AbpUsers_CreatorUserId",
                table: "VacationTypes");

            migrationBuilder.DropIndex(
                name: "IX_VacationTypes_CreatorUserId",
                table: "VacationTypes");
        }
    }
}
