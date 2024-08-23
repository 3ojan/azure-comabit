using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class SellerCompanyToAreaSubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioAreaSubCategorySellerCompany",
                columns: table => new
                {
                    PortfolioAreaSubCategoriesPortfolioAreaSubCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesGuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreaSubCategorySellerCompany", x => new { x.PortfolioAreaSubCategoriesPortfolioAreaSubCategoryId, x.SellerCompaniesGuid });
                    table.ForeignKey(
                        name: "FK_PortfolioAreaSubCategorySellerCompany_Companies_SellerCompa~",
                        column: x => x.SellerCompaniesGuid,
                        principalTable: "Companies",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioAreaSubCategorySellerCompany_PortfolioAreaSubCateg~",
                        column: x => x.PortfolioAreaSubCategoriesPortfolioAreaSubCategoryId,
                        principalTable: "PortfolioAreaSubCategories",
                        principalColumn: "PortfolioAreaSubCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAreaSubCategorySellerCompany_SellerCompaniesGuid",
                table: "PortfolioAreaSubCategorySellerCompany",
                column: "SellerCompaniesGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioAreaSubCategorySellerCompany");
        }
    }
}
