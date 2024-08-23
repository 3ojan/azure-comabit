using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class addupdatefieldsonbuyerprojectinquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "BuyerProjectInquiry",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BuyerProjectInquiry",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "BuyerProjectInquiry",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "BuyerProjectInquiry");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BuyerProjectInquiry");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BuyerProjectInquiry");
        }
    }
}
