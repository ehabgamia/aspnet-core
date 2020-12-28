using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class modifiedenumforvacationunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Unit",
                table: "VacationTypes",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<byte>(
                name: "VacationUnit",
                table: "VacationTypes",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VacationUnit",
                table: "VacationTypes");

            migrationBuilder.AlterColumn<byte>(
                name: "Unit",
                table: "VacationTypes",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
