using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class ChangeCompanyGuidToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyGuid",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SellerCompaniesGuid",
                table: "PortfolioAreaSubCategorySellerCompany",
                newName: "SellerCompaniesId");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioAreaSubCategorySellerCompany_SellerCompaniesGuid",
                table: "PortfolioAreaSubCategorySellerCompany",
                newName: "IX_PortfolioAreaSubCategorySellerCompany_SellerCompaniesId");

            migrationBuilder.RenameColumn(
                name: "SellerCompaniesGuid",
                table: "PortfolioAreaCategorySellerCompany",
                newName: "SellerCompaniesId");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioAreaCategorySellerCompany_SellerCompaniesGuid",
                table: "PortfolioAreaCategorySellerCompany",
                newName: "IX_PortfolioAreaCategorySellerCompany_SellerCompaniesId");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Companies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CompanyGuid",
                table: "AspNetUsers",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CompanyGuid",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CompanyId");

            migrationBuilder.RenameColumn(
                name: "SellerCompanyGuid",
                table: "AdditionalPortfolioAreaCategoryTags",
                newName: "SellerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalPortfolioAreaCategoryTags_SellerCompanyGuid",
                table: "AdditionalPortfolioAreaCategoryTags",
                newName: "IX_AdditionalPortfolioAreaCategoryTags_SellerCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SellerCompaniesId",
                table: "PortfolioAreaSubCategorySellerCompany",
                newName: "SellerCompaniesGuid");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioAreaSubCategorySellerCompany_SellerCompaniesId",
                table: "PortfolioAreaSubCategorySellerCompany",
                newName: "IX_PortfolioAreaSubCategorySellerCompany_SellerCompaniesGuid");

            migrationBuilder.RenameColumn(
                name: "SellerCompaniesId",
                table: "PortfolioAreaCategorySellerCompany",
                newName: "SellerCompaniesGuid");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioAreaCategorySellerCompany_SellerCompaniesId",
                table: "PortfolioAreaCategorySellerCompany",
                newName: "IX_PortfolioAreaCategorySellerCompany_SellerCompaniesGuid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Companies",
                newName: "Guid");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "AspNetUsers",
                newName: "CompanyGuid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CompanyGuid");

            migrationBuilder.RenameColumn(
                name: "SellerCompanyId",
                table: "AdditionalPortfolioAreaCategoryTags",
                newName: "SellerCompanyGuid");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalPortfolioAreaCategoryTags_SellerCompanyId",
                table: "AdditionalPortfolioAreaCategoryTags",
                newName: "IX_AdditionalPortfolioAreaCategoryTags_SellerCompanyGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyGuid",
                table: "AspNetUsers",
                column: "CompanyGuid",
                principalTable: "Companies",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
