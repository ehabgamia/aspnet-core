using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class added_getEmployeeQuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "SapCode",
            //    table: "VacationsGetEmpData");

            //migrationBuilder.CreateTable(
            //    name: "VacationsGetEmpQuotaData",
            //    columns: table => new
            //    {
            //        emp_username = table.Column<string>(nullable: false),
            //        Id = table.Column<int>(nullable: false),
            //        AbsenceQuota = table.Column<float>(nullable: false),
            //        Absence = table.Column<float>(nullable: false),
            //        AbsenceBalance = table.Column<float>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VacationsGetEmpQuotaData", x => x.emp_username);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationsGetEmpQuotaData");

            //migrationBuilder.AddColumn<int>(
            //    name: "SapCode",
            //    table: "VacationsGetEmpData",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
