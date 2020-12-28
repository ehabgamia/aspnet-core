using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class Added_Announcements_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
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
                    Subject = table.Column<string>(maxLength: 150, nullable: false),
                    Message = table.Column<string>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    AnnouncementTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_AnnouncementTypes_AnnouncementTypeId",
                        column: x => x.AnnouncementTypeId,
                        principalTable: "AnnouncementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateTable(
            //    name: "RestInfos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        LastModificationTime = table.Column<DateTime>(nullable: true),
            //        LastModifierUserId = table.Column<long>(nullable: true),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        DeleterUserId = table.Column<long>(nullable: true),
            //        DeletionTime = table.Column<DateTime>(nullable: true),
            //        Country = table.Column<string>(nullable: true),
            //        City = table.Column<string>(nullable: true),
            //        Area = table.Column<string>(nullable: true),
            //        Building = table.Column<string>(nullable: true),
            //        AdminPerson = table.Column<string>(nullable: true),
            //        isAvailable = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RestInfos", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RestSchedules",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        LastModificationTime = table.Column<DateTime>(nullable: true),
            //        LastModifierUserId = table.Column<long>(nullable: true),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        DeleterUserId = table.Column<long>(nullable: true),
            //        DeletionTime = table.Column<DateTime>(nullable: true),
            //        CurrentDate = table.Column<DateTime>(nullable: false),
            //        Total = table.Column<int>(nullable: false),
            //        Day = table.Column<byte>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RestSchedules", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RestItems",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        LastModificationTime = table.Column<DateTime>(nullable: true),
            //        LastModifierUserId = table.Column<long>(nullable: true),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        DeleterUserId = table.Column<long>(nullable: true),
            //        DeletionTime = table.Column<DateTime>(nullable: true),
            //        ItemDesc = table.Column<string>(nullable: true),
            //        ItemCode = table.Column<string>(nullable: true),
            //        CostPrice = table.Column<double>(nullable: false),
            //        SalesPrice = table.Column<double>(nullable: false),
            //        Picture = table.Column<string>(nullable: true),
            //        Category = table.Column<byte>(nullable: false),
            //        RestScheduleId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RestItems", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RestItems_RestSchedules_RestScheduleId",
            //            column: x => x.RestScheduleId,
            //            principalTable: "RestSchedules",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_AnnouncementTypeId",
                table: "Announcements",
                column: "AnnouncementTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RestItems_RestScheduleId",
            //    table: "RestItems",
            //    column: "RestScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            //migrationBuilder.DropTable(
            //    name: "RestInfos");

            //migrationBuilder.DropTable(
            //    name: "RestItems");

            //migrationBuilder.DropTable(
            //    name: "RestSchedules");
        }
    }
}
