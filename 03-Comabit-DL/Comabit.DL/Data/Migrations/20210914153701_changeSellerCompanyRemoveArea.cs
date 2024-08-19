using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class changeSellerCompanyRemoveArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PortfolioAreaSubCategoriesId",
                table: "PortfolioSubCategorySellerCompany",
                newName: "PortfolioSubCategoriesId");

            migrationBuilder.RenameColumn(
                name: "PortfolioAreaCategoriesId",
                table: "PortfolioCategorySellerCompany",
                newName: "PortfolioCategoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PortfolioSubCategoriesId",
                table: "PortfolioSubCategorySellerCompany",
                newName: "PortfolioAreaSubCategoriesId");

            migrationBuilder.RenameColumn(
                name: "PortfolioCategoriesId",
                table: "PortfolioCategorySellerCompany",
                newName: "PortfolioAreaCategoriesId");
        }
    }
}
