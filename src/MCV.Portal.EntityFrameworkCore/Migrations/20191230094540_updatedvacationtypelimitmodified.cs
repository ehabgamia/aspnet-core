using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class updatedvacationtypelimitmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "VacationTypes");

            migrationBuilder.DropColumn(
                name: "VacationTypeLimit",
                table: "VacationTypes");

            migrationBuilder.AddColumn<int>(
                name: "VacationTypeLimitId",
                table: "VacationTypes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VacationTypeLimit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Unit = table.Column<byte>(nullable: false),
                    Limit = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationTypeLimit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationTypes_VacationTypeLimitId",
                table: "VacationTypes",
                column: "VacationTypeLimitId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationTypes_VacationTypeLimit_VacationTypeLimitId",
                table: "VacationTypes",
                column: "VacationTypeLimitId",
                principalTable: "VacationTypeLimit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationTypes_VacationTypeLimit_VacationTypeLimitId",
                table: "VacationTypes");

            migrationBuilder.DropTable(
                name: "VacationTypeLimit");

            migrationBuilder.DropIndex(
                name: "IX_VacationTypes_VacationTypeLimitId",
                table: "VacationTypes");

            migrationBuilder.DropColumn(
                name: "VacationTypeLimitId",
                table: "VacationTypes");

            migrationBuilder.AddColumn<byte>(
                name: "Unit",
                table: "VacationTypes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "VacationTypeLimit",
                table: "VacationTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
