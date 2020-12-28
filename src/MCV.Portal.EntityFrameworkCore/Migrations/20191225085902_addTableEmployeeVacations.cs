using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class addTableEmployeeVacations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VacationsHRSystem",
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
                    Requester = table.Column<string>(nullable: true),
                    VacationTypeId = table.Column<int>(nullable: false),
                    EmpSAPCode = table.Column<int>(nullable: false),
                    VacationFrom = table.Column<DateTime>(nullable: false),
                    VacationTo = table.Column<DateTime>(nullable: false),
                    Period = table.Column<decimal>(nullable: false),
                    OnBehalfSAPCode = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationsHRSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationsHRSystem_VacationTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationsHRSystem_VacationTypeId",
                table: "VacationsHRSystem",
                column: "VacationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationsHRSystem");
        }
    }
}
