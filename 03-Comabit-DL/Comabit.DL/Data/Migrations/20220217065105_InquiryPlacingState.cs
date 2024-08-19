using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class InquiryPlacingState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Inquiries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PlacingState",
                table: "Inquiries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "PlacingState",
                table: "Inquiries");
        }
    }
}
