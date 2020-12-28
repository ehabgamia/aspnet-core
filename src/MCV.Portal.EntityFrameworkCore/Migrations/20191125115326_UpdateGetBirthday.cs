using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class UpdateGetBirthday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_BirthdayToday",
            //    table: "BirthdayToday");

            //migrationBuilder.RenameTable(
            //    name: "BirthdayToday",
            //    newName: "GetBirthdaysToday");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_GetBirthdaysToday",
            //    table: "GetBirthdaysToday",
            //    column: "BirthName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GetBirthdaysToday",
                table: "GetBirthdaysToday");

            migrationBuilder.RenameTable(
                name: "GetBirthdaysToday",
                newName: "BirthdayToday");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BirthdayToday",
                table: "BirthdayToday",
                column: "BirthName");
        }
    }
}
