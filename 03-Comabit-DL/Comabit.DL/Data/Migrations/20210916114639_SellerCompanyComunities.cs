using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class SellerCompanyComunities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalPortfolioCategoryTags_PortfolioCategories_Portfol~",
                table: "AdditionalPortfolioCategoryTags");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerCompanyId",
                table: "Communities",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioCategoryId",
                table: "AdditionalPortfolioCategoryTags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Communities_SellerCompanyId",
                table: "Communities",
                column: "SellerCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalPortfolioCategoryTags_PortfolioCategories_Portfol~",
                table: "AdditionalPortfolioCategoryTags",
                column: "PortfolioCategoryId",
                principalTable: "PortfolioCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Companies_SellerCompanyId",
                table: "Communities",
                column: "SellerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalPortfolioCategoryTags_PortfolioCategories_Portfol~",
                table: "AdditionalPortfolioCategoryTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Companies_SellerCompanyId",
                table: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Communities_SellerCompanyId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "SellerCompanyId",
                table: "Communities");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioCategoryId",
                table: "AdditionalPortfolioCategoryTags",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalPortfolioCategoryTags_PortfolioCategories_Portfol~",
                table: "AdditionalPortfolioCategoryTags",
                column: "PortfolioCategoryId",
                principalTable: "PortfolioCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
