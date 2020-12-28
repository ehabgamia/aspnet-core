using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class updatevacationhrsystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationsHRSystem_VacationTypes_VacationTypeId",
                table: "VacationsHRSystem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacationsHRSystem",
                table: "VacationsHRSystem");

            migrationBuilder.RenameTable(
                name: "VacationsHRSystem",
                newName: "VacationHRSystem");

            migrationBuilder.RenameIndex(
                name: "IX_VacationsHRSystem_VacationTypeId",
                table: "VacationHRSystem",
                newName: "IX_VacationHRSystem_VacationTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacationHRSystem",
                table: "VacationHRSystem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationHRSystem_VacationTypes_VacationTypeId",
                table: "VacationHRSystem",
                column: "VacationTypeId",
                principalTable: "VacationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationHRSystem_VacationTypes_VacationTypeId",
                table: "VacationHRSystem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacationHRSystem",
                table: "VacationHRSystem");

            migrationBuilder.RenameTable(
                name: "VacationHRSystem",
                newName: "VacationsHRSystem");

            migrationBuilder.RenameIndex(
                name: "IX_VacationHRSystem_VacationTypeId",
                table: "VacationsHRSystem",
                newName: "IX_VacationsHRSystem_VacationTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacationsHRSystem",
                table: "VacationsHRSystem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationsHRSystem_VacationTypes_VacationTypeId",
                table: "VacationsHRSystem",
                column: "VacationTypeId",
                principalTable: "VacationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
