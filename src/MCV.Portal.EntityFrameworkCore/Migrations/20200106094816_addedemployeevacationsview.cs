using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCV.Portal.Migrations
{
    public partial class addedemployeevacationsview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "VacationsEmployeeView",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        VacationTypeId = table.Column<int>(nullable: false),
            //        EmpSAPCode = table.Column<int>(nullable: false),
            //        VacationFrom = table.Column<DateTime>(nullable: false),
            //        VacationTo = table.Column<DateTime>(nullable: false),
            //        Period = table.Column<decimal>(nullable: false),
            //        OnBehalfSAPCode = table.Column<int>(nullable: false),
            //        Status = table.Column<byte>(nullable: false),
            //        Reason = table.Column<string>(nullable: true),
            //        Requester = table.Column<string>(nullable: true),
            //        Manager = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VacationsEmployeeView", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_VacationsEmployeeView_VacationTypes_VacationTypeId",
            //            column: x => x.VacationTypeId,
            //            principalTable: "VacationTypes",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_VacationsEmployeeView_VacationTypeId",
            //    table: "VacationsEmployeeView",
            //    column: "VacationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "VacationsEmployeeView");
        }
    }
}
