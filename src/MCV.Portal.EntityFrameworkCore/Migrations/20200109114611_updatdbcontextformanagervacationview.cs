using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class updatdbcontextformanagervacationview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_VacationsGetManagerVacations",
            //    table: "VacationsGetManagerVacations");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "VacationsGetManagerVacations",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_VacationsGetManagerVacations",
            //    table: "VacationsGetManagerVacations",
            //    column: "Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VacationsGetManagerVacations_EmployeeVacationId",
            //    table: "VacationsGetManagerVacations",
            //    column: "EmployeeVacationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_VacationsGetManagerVacations",
            //    table: "VacationsGetManagerVacations");

            //migrationBuilder.DropIndex(
            //    name: "IX_VacationsGetManagerVacations_EmployeeVacationId",
            //    table: "VacationsGetManagerVacations");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "VacationsGetManagerVacations",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_VacationsGetManagerVacations",
            //    table: "VacationsGetManagerVacations",
            //    column: "EmployeeVacationId");
        }
    }
}
