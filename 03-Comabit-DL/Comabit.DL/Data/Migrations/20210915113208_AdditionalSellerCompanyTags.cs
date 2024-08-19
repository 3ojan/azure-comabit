using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class AdditionalSellerCompanyTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalPortfolioCategoryTags_Companies_SellerCompanyId",
                table: "AdditionalPortfolioCategoryTags");

            migrationBuilder.AlterColumn<Guid>(
                name: "SellerCompanyId",
                table: "AdditionalPortfolioCategoryTags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalPortfolioCategoryTags_Companies_SellerCompanyId",
                table: "AdditionalPortfolioCategoryTags",
                column: "SellerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalPortfolioCategoryTags_Companies_SellerCompanyId",
                table: "AdditionalPortfolioCategoryTags");

            migrationBuilder.AlterColumn<Guid>(
                name: "SellerCompanyId",
                table: "AdditionalPortfolioCategoryTags",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalPortfolioCategoryTags_Companies_SellerCompanyId",
                table: "AdditionalPortfolioCategoryTags",
                column: "SellerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
