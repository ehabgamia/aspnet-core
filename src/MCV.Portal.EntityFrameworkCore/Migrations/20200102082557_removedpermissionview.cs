using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class removedpermissionview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_VacationsHavePermission",
            //    table: "VacationsHavePermission");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "VacationsHavePermission",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Period",
            //    table: "VacationsHavePermission",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_VacationsHavePermission",
            //    table: "VacationsHavePermission",
            //    column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_VacationsHavePermission",
            //    table: "VacationsHavePermission");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Period",
            //    table: "VacationsHavePermission",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "VacationsHavePermission",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_VacationsHavePermission",
            //    table: "VacationsHavePermission",
            //    column: "Period");
        }
    }
}
