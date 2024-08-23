using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class AddFixCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Company_CompanyGuid",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyGuid",
                table: "AspNetUsers",
                column: "CompanyGuid",
                principalTable: "Companies",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyGuid",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Company_CompanyGuid",
                table: "AspNetUsers",
                column: "CompanyGuid",
                principalTable: "Company",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
