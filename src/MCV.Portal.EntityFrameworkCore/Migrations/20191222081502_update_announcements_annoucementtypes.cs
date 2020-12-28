using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class update_announcements_annoucementtypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AnnouncementTypes_AnnouncementTypeId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Announcements");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementTypeId",
                table: "Announcements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AnnouncementTypes_AnnouncementTypeId",
                table: "Announcements",
                column: "AnnouncementTypeId",
                principalTable: "AnnouncementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AnnouncementTypes_AnnouncementTypeId",
                table: "Announcements");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementTypeId",
                table: "Announcements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AnnouncementTypes_AnnouncementTypeId",
                table: "Announcements",
                column: "AnnouncementTypeId",
                principalTable: "AnnouncementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
