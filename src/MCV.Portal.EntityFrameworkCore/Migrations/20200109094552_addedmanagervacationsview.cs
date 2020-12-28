using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class addedmanagervacationsview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "VacationsGetManagerVacations",
            //    columns: table => new
            //    {
            //        EmployeeVacationId = table.Column<int>(nullable: false),
            //        Id = table.Column<int>(nullable: false),
            //        VacationTypeId = table.Column<int>(nullable: false),
            //        Status = table.Column<byte>(nullable: false),
            //        EmployeeSAPCode = table.Column<int>(nullable: false),
            //        VacationFrom = table.Column<DateTime>(nullable: false),
            //        VacationTo = table.Column<DateTime>(nullable: false),
            //        Period = table.Column<string>(nullable: true),
            //        OnBehalf = table.Column<int>(nullable: false),
            //        EntryDate = table.Column<DateTime>(nullable: false),
            //        VacationTypeID = table.Column<int>(nullable: false),
            //        ForType = table.Column<string>(nullable: true),
            //        Employee = table.Column<string>(nullable: true),
            //        OnBehalfName = table.Column<string>(nullable: true),
            //        OrgName = table.Column<string>(nullable: true),
            //        JobTitle = table.Column<string>(nullable: true),
            //        OnBehalfCCManagerVacation = table.Column<string>(nullable: true),
            //        Requester = table.Column<string>(nullable: true),
            //        Reason = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VacationsGetManagerVacations", x => x.EmployeeVacationId);
            //        table.ForeignKey(
            //            name: "FK_VacationsGetManagerVacations_VacationHRSystem_EmployeeVacationId",
            //            column: x => x.EmployeeVacationId,
            //            principalTable: "VacationHRSystem",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_VacationsGetManagerVacations_VacationTypes_VacationTypeId",
            //            column: x => x.VacationTypeId,
            //            principalTable: "VacationTypes",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_VacationsGetManagerVacations_VacationTypeId",
            //    table: "VacationsGetManagerVacations",
            //    column: "VacationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "VacationsGetManagerVacations");
        }
    }
}
