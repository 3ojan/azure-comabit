using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class MatchScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_BuyerInquire_InquiryId",
                table: "Matchs");

            migrationBuilder.DropTable(
                name: "BuyerInquiryPortfolioCategory");

            migrationBuilder.DropTable(
                name: "BuyerInquiryPortfolioSubCategory");

            migrationBuilder.DropTable(
                name: "BuyerInquire");

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Matchs",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_BuyerProjectInquiry_InquiryId",
                table: "Matchs",
                column: "InquiryId",
                principalTable: "BuyerProjectInquiry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_BuyerProjectInquiry_InquiryId",
                table: "Matchs");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Matchs");

            migrationBuilder.CreateTable(
                name: "BuyerInquire",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerCompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    ContactClerk = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    ProjectDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerInquire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerInquire_Companies_BuyerCompanyId",
                        column: x => x.BuyerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerInquiryPortfolioCategory",
                columns: table => new
                {
                    BuyerInquireId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioCategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerInquiryPortfolioCategory", x => new { x.BuyerInquireId, x.PortfolioCategoriesId });
                    table.ForeignKey(
                        name: "FK_BuyerInquiryPortfolioCategory_BuyerInquire_BuyerInquireId",
                        column: x => x.BuyerInquireId,
                        principalTable: "BuyerInquire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerInquiryPortfolioCategory_PortfolioCategories_Portfolio~",
                        column: x => x.PortfolioCategoriesId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerInquiryPortfolioSubCategory",
                columns: table => new
                {
                    BuyerInquireId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioSubCategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerInquiryPortfolioSubCategory", x => new { x.BuyerInquireId, x.PortfolioSubCategoriesId });
                    table.ForeignKey(
                        name: "FK_BuyerInquiryPortfolioSubCategory_BuyerInquire_BuyerInquireId",
                        column: x => x.BuyerInquireId,
                        principalTable: "BuyerInquire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerInquiryPortfolioSubCategory_PortfolioSubCategories_Por~",
                        column: x => x.PortfolioSubCategoriesId,
                        principalTable: "PortfolioSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerInquire_BuyerCompanyId",
                table: "BuyerInquire",
                column: "BuyerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerInquiryPortfolioCategory_PortfolioCategoriesId",
                table: "BuyerInquiryPortfolioCategory",
                column: "PortfolioCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerInquiryPortfolioSubCategory_PortfolioSubCategoriesId",
                table: "BuyerInquiryPortfolioSubCategory",
                column: "PortfolioSubCategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_BuyerInquire_InquiryId",
                table: "Matchs",
                column: "InquiryId",
                principalTable: "BuyerInquire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
