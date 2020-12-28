using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class AddedBirthdays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "BirthdayToday",
            //    columns: table => new
            //    {
            //        BirthName = table.Column<string>(nullable: false),
            //        Id = table.Column<int>(nullable: false),
            //        ImgPath = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BirthdayToday", x => x.BirthName);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirthdayToday");
        }
    }
}
