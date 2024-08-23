using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class AddAdditionalTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalPortfolioAreaCategoryTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioAreaCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    SellerCompanyGuid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalPortfolioAreaCategoryTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalPortfolioAreaCategoryTags_Companies_SellerCompany~",
                        column: x => x.SellerCompanyGuid,
                        principalTable: "Companies",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalPortfolioAreaCategoryTags_PortfolioAreaCategories~",
                        column: x => x.PortfolioAreaCategoryId,
                        principalTable: "PortfolioAreaCategories",
                        principalColumn: "PortfolioAreaCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalPortfolioAreaCategoryTags_PortfolioAreaCategoryId",
                table: "AdditionalPortfolioAreaCategoryTags",
                column: "PortfolioAreaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalPortfolioAreaCategoryTags_SellerCompanyGuid",
                table: "AdditionalPortfolioAreaCategoryTags",
                column: "SellerCompanyGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalPortfolioAreaCategoryTags");
        }
    }
}
