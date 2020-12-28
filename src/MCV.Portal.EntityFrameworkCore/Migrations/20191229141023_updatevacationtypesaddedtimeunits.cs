using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class updatevacationtypesaddedtimeunits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Unit",
                table: "VacationTypes",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "VacationTypeLimit",
                table: "VacationTypes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "VacationTypes");

            migrationBuilder.DropColumn(
                name: "VacationTypeLimit",
                table: "VacationTypes");
        }
    }
}
