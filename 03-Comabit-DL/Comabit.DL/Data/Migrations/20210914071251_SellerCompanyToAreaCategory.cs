using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class SellerCompanyToAreaCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioAreaCategorySellerCompany",
                columns: table => new
                {
                    PortfolioAreaCategoriesPortfolioAreaCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesGuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreaCategorySellerCompany", x => new { x.PortfolioAreaCategoriesPortfolioAreaCategoryId, x.SellerCompaniesGuid });
                    table.ForeignKey(
                        name: "FK_PortfolioAreaCategorySellerCompany_Companies_SellerCompanie~",
                        column: x => x.SellerCompaniesGuid,
                        principalTable: "Companies",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioAreaCategorySellerCompany_PortfolioAreaCategories_~",
                        column: x => x.PortfolioAreaCategoriesPortfolioAreaCategoryId,
                        principalTable: "PortfolioAreaCategories",
                        principalColumn: "PortfolioAreaCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAreaCategorySellerCompany_SellerCompaniesGuid",
                table: "PortfolioAreaCategorySellerCompany",
                column: "SellerCompaniesGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioAreaCategorySellerCompany");
        }
    }
}
