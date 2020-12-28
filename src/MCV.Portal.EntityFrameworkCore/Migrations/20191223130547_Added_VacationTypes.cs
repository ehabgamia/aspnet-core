using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class Added_VacationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VacationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TypeOfVacation = table.Column<string>(nullable: true),
                    SAPCodeType = table.Column<int>(nullable: false),
                    ServiceDeskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementTypes_CreatorUserId",
                table: "AnnouncementTypes",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CreatorUserId",
                table: "Announcements",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AbpUsers_CreatorUserId",
                table: "Announcements",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementTypes_AbpUsers_CreatorUserId",
                table: "AnnouncementTypes",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AbpUsers_CreatorUserId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementTypes_AbpUsers_CreatorUserId",
                table: "AnnouncementTypes");

            migrationBuilder.DropTable(
                name: "VacationTypes");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementTypes_CreatorUserId",
                table: "AnnouncementTypes");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_CreatorUserId",
                table: "Announcements");
        }
    }
}
