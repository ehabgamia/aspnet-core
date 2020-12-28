using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class addedsapemphierarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SAPEmpHierarchy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SAPCode = table.Column<int>(nullable: false),
                    OrgCode = table.Column<int>(nullable: false),
                    SLevel1 = table.Column<string>(nullable: true),
                    SLevel1SAPCode = table.Column<int>(nullable: false),
                    SLevel2 = table.Column<string>(nullable: true),
                    SLevel2SAPCode = table.Column<int>(nullable: false),
                    SLevel3 = table.Column<string>(nullable: true),
                    SLevel3SAPCode = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAPEmpHierarchy", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SAPEmpHierarchy");
        }
    }
}
