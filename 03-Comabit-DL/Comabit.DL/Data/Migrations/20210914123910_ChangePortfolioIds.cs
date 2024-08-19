using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class ChangePortfolioIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaCategories_PortfolioAreas_PortfolioAreaId",
                table: "PortfolioAreaCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaSubCategories_PortfolioAreaCategories_Portfoli~",
                table: "PortfolioAreaSubCategories");

            migrationBuilder.RenameColumn(
                name: "PortfolioAreaSubCategoriesPortfolioAreaSubCategoryId",
                table: "PortfolioAreaSubCategorySellerCompany",
                newName: "PortfolioAreaSubCategoriesId");

            migrationBuilder.RenameColumn(
                name: "PortfolioAreaSubCategoryId",
                table: "PortfolioAreaSubCategories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PortfolioAreaId",
                table: "PortfolioAreas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PortfolioAreaCategoriesPortfolioAreaCategoryId",
                table: "PortfolioAreaCategorySellerCompany",
                newName: "PortfolioAreaCategoriesId");

            migrationBuilder.RenameColumn(
                name: "PortfolioAreaCategoryId",
                table: "PortfolioAreaCategories",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioAreaCategoryId",
                table: "PortfolioAreaSubCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioAreaId",
                table: "PortfolioAreaCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreaCategories_PortfolioAreas_PortfolioAreaId",
                table: "PortfolioAreaCategories",
                column: "PortfolioAreaId",
                principalTable: "PortfolioAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreaSubCategories_PortfolioAreaCategories_Portfoli~",
                table: "PortfolioAreaSubCategories",
                column: "PortfolioAreaCategoryId",
                principalTable: "PortfolioAreaCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaCategories_PortfolioAreas_PortfolioAreaId",
                table: "PortfolioAreaCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaSubCategories_PortfolioAreaCategories_Portfoli~",
                table: "PortfolioAreaSubCategories");

            migrationBuilder.RenameColumn(
                name: "PortfolioAreaSubCategoriesId",
                table: "PortfolioAreaSubCategorySellerCompany",
                newName: "PortfolioAreaSubCategoriesPortfolioAreaSubCategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PortfolioAreaSubCategories",
                newName: "PortfolioAreaSubCategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PortfolioAreas",
                newName: "PortfolioAreaId");

            migrationBuilder.RenameColumn(
                name: "PortfolioAreaCategoriesId",
                table: "PortfolioAreaCategorySellerCompany",
                newName: "PortfolioAreaCategoriesPortfolioAreaCategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PortfolioAreaCategories",
                newName: "PortfolioAreaCategoryId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioAreaCategoryId",
                table: "PortfolioAreaSubCategories",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioAreaId",
                table: "PortfolioAreaCategories",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreaCategories_PortfolioAreas_PortfolioAreaId",
                table: "PortfolioAreaCategories",
                column: "PortfolioAreaId",
                principalTable: "PortfolioAreas",
                principalColumn: "PortfolioAreaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreaSubCategories_PortfolioAreaCategories_Portfoli~",
                table: "PortfolioAreaSubCategories",
                column: "PortfolioAreaCategoryId",
                principalTable: "PortfolioAreaCategories",
                principalColumn: "PortfolioAreaCategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
