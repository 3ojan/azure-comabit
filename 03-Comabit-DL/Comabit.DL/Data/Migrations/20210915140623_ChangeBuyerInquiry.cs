using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class ChangeBuyerInquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BuyerCompanyId",
                table: "BuyerInquire",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BuyerInquire_BuyerCompanyId",
                table: "BuyerInquire",
                column: "BuyerCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyerInquire_Companies_BuyerCompanyId",
                table: "BuyerInquire",
                column: "BuyerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyerInquire_Companies_BuyerCompanyId",
                table: "BuyerInquire");

            migrationBuilder.DropIndex(
                name: "IX_BuyerInquire_BuyerCompanyId",
                table: "BuyerInquire");

            migrationBuilder.DropColumn(
                name: "BuyerCompanyId",
                table: "BuyerInquire");
        }
    }
}
