using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class changeAdditionalPortfolioCategoryTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PortfolioAreaCategoryId",
                table: "AdditionalPortfolioCategoryTags",
                newName: "PortfolioCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalPortfolioCategoryTags_PortfolioAreaCategoryId",
                table: "AdditionalPortfolioCategoryTags",
                newName: "IX_AdditionalPortfolioCategoryTags_PortfolioCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PortfolioCategoryId",
                table: "AdditionalPortfolioCategoryTags",
                newName: "PortfolioAreaCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalPortfolioCategoryTags_PortfolioCategoryId",
                table: "AdditionalPortfolioCategoryTags",
                newName: "IX_AdditionalPortfolioCategoryTags_PortfolioAreaCategoryId");
        }
    }
}
