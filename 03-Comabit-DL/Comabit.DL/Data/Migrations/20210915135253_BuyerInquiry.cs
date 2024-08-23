using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class BuyerInquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyerInquire",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    ProjectDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    ContactClerk = table.Column<string>(type: "text", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerInquire", x => x.Id);
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
                name: "IX_BuyerInquiryPortfolioCategory_PortfolioCategoriesId",
                table: "BuyerInquiryPortfolioCategory",
                column: "PortfolioCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerInquiryPortfolioSubCategory_PortfolioSubCategoriesId",
                table: "BuyerInquiryPortfolioSubCategory",
                column: "PortfolioSubCategoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerInquiryPortfolioCategory");

            migrationBuilder.DropTable(
                name: "BuyerInquiryPortfolioSubCategory");

            migrationBuilder.DropTable(
                name: "BuyerInquire");
        }
    }
}
