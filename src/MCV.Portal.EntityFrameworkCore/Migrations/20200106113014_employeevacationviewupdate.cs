using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class employeevacationviewupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "OnBehalfSAPCode",
            //    table: "VacationsEmployeeView");

            //migrationBuilder.DropColumn(
            //    name: "Period",
            //    table: "VacationsEmployeeView");

            //migrationBuilder.AddColumn<string>(
            //    name: "OnBehalfEmp",
            //    table: "VacationsEmployeeView",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "OnBehalfEmp",
            //    table: "VacationsEmployeeView");

            //migrationBuilder.AddColumn<int>(
            //    name: "OnBehalfSAPCode",
            //    table: "VacationsEmployeeView",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "Period",
            //    table: "VacationsEmployeeView",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    defaultValue: 0m);
        }
    }
}
