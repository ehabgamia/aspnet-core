using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class addedgetempdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "VacationsGetEmpData",
            //    columns: table => new
            //    {
            //        BirthName = table.Column<string>(nullable: false),
            //        Id = table.Column<int>(nullable: false),
            //        SapCode = table.Column<int>(nullable: false),
            //        Position = table.Column<string>(nullable: true),
            //        EmployeeType = table.Column<int>(nullable: false),
            //        OrgName = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VacationsGetEmpData", x => x.BirthName);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationsGetEmpData");
        }
    }
}
