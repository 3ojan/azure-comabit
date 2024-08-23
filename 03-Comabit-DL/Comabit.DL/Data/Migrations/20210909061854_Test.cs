using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyGuid",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyGuid",
                table: "AspNetUsers",
                column: "CompanyGuid",
                principalTable: "Companies",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyGuid",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyGuid",
                table: "AspNetUsers",
                column: "CompanyGuid",
                principalTable: "Companies",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
