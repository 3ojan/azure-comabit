using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class _20210901_Company_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Company",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "Company",
                newName: "PostalCode");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Company",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Company",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Company",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Company",
                newName: "Address1");
        }
    }
}
