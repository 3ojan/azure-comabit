using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class AddUniqueIndexInAdditionalCategoryTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdditionalPortfolioCategoryTags_PortfolioCategoryId",
                table: "AdditionalPortfolioCategoryTags");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalPortfolioCategoryTags_PortfolioCategoryId_SellerC~",
                table: "AdditionalPortfolioCategoryTags",
                columns: new[] { "PortfolioCategoryId", "SellerCompanyId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdditionalPortfolioCategoryTags_PortfolioCategoryId_SellerC~",
                table: "AdditionalPortfolioCategoryTags");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalPortfolioCategoryTags_PortfolioCategoryId",
                table: "AdditionalPortfolioCategoryTags",
                column: "PortfolioCategoryId");
        }
    }
}
