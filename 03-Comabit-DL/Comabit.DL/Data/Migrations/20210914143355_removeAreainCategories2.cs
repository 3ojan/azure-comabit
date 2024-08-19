using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class removeAreainCategories2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalPortfolioAreaCategoryTags_PortfolioAreaCategories~",
                table: "AdditionalPortfolioAreaCategoryTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaCategories_PortfolioAreas_PortfolioAreaId",
                table: "PortfolioAreaCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaCategorySellerCompany_PortfolioAreaCategories_~",
                table: "PortfolioAreaCategorySellerCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaSubCategories_PortfolioAreaCategories_Portfoli~",
                table: "PortfolioAreaSubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioAreaCategories",
                table: "PortfolioAreaCategories");

            migrationBuilder.RenameTable(
                name: "PortfolioAreaCategories",
                newName: "PortfolioAreasCategories");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioAreaCategories_PortfolioAreaId",
                table: "PortfolioAreasCategories",
                newName: "IX_PortfolioAreasCategories_PortfolioAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioAreasCategories",
                table: "PortfolioAreasCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalPortfolioAreaCategoryTags_PortfolioAreasCategorie~",
                table: "AdditionalPortfolioAreaCategoryTags",
                column: "PortfolioAreaCategoryId",
                principalTable: "PortfolioAreasCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreaCategorySellerCompany_PortfolioAreasCategories~",
                table: "PortfolioAreaCategorySellerCompany",
                column: "PortfolioAreaCategoriesId",
                principalTable: "PortfolioAreasCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreasCategories_PortfolioAreas_PortfolioAreaId",
                table: "PortfolioAreasCategories",
                column: "PortfolioAreaId",
                principalTable: "PortfolioAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreaSubCategories_PortfolioAreasCategories_Portfol~",
                table: "PortfolioAreaSubCategories",
                column: "PortfolioAreaCategoryId",
                principalTable: "PortfolioAreasCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalPortfolioAreaCategoryTags_PortfolioAreasCategorie~",
                table: "AdditionalPortfolioAreaCategoryTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaCategorySellerCompany_PortfolioAreasCategories~",
                table: "PortfolioAreaCategorySellerCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreasCategories_PortfolioAreas_PortfolioAreaId",
                table: "PortfolioAreasCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAreaSubCategories_PortfolioAreasCategories_Portfol~",
                table: "PortfolioAreaSubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioAreasCategories",
                table: "PortfolioAreasCategories");

            migrationBuilder.RenameTable(
                name: "PortfolioAreasCategories",
                newName: "PortfolioAreaCategories");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioAreasCategories_PortfolioAreaId",
                table: "PortfolioAreaCategories",
                newName: "IX_PortfolioAreaCategories_PortfolioAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioAreaCategories",
                table: "PortfolioAreaCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalPortfolioAreaCategoryTags_PortfolioAreaCategories~",
                table: "AdditionalPortfolioAreaCategoryTags",
                column: "PortfolioAreaCategoryId",
                principalTable: "PortfolioAreaCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreaCategories_PortfolioAreas_PortfolioAreaId",
                table: "PortfolioAreaCategories",
                column: "PortfolioAreaId",
                principalTable: "PortfolioAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAreaCategorySellerCompany_PortfolioAreaCategories_~",
                table: "PortfolioAreaCategorySellerCompany",
                column: "PortfolioAreaCategoriesId",
                principalTable: "PortfolioAreaCategories",
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
    }
}
