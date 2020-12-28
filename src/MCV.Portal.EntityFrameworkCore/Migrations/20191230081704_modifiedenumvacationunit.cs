using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class modifiedenumvacationunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VacationUnit",
                table: "VacationTypes");

            migrationBuilder.AlterColumn<byte>(
                name: "Unit",
                table: "VacationTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Unit",
                table: "VacationTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<byte>(
                name: "VacationUnit",
                table: "VacationTypes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
