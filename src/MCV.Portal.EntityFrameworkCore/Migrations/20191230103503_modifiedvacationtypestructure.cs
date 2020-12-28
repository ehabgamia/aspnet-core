using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class modifiedvacationtypestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "Limit",
                table: "VacationTypes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "Unit",
                table: "VacationTypes",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "VacationTypes");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "VacationTypes");

            migrationBuilder.AddColumn<int>(
                name: "VacationTypeLimitId",
                table: "VacationTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VacationTypeLimit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    Limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<byte>(type: "tinyint", nullable: false)
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
    }
}
