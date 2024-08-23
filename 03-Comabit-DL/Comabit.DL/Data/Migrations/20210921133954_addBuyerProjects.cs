using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Comabit.DL.Migrations
{
    public partial class addBuyerProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyerProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerCompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    ContactClerk = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerProject_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyerProject_Companies_BuyerCompanyId",
                        column: x => x.BuyerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerProjectInquiry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InquiryNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuyerProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeadlineInfo = table.Column<string>(type: "text", nullable: true),
                    DeliveryPlace = table.Column<string>(type: "text", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeliveryInfo = table.Column<string>(type: "text", nullable: true),
                    AddidtionalTags = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Requirements = table.Column<string>(type: "text", nullable: true),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProjectInquiry", x => x.Id);
                    table.UniqueConstraint("AK_BuyerProjectInquiry_InquiryNumber", x => x.InquiryNumber);
                    table.ForeignKey(
                        name: "FK_BuyerProjectInquiry_BuyerProject_BuyerProjectId",
                        column: x => x.BuyerProjectId,
                        principalTable: "BuyerProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerProjectInquiryPortfolioCategory",
                columns: table => new
                {
                    BuyerProjectInquireId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioCategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProjectInquiryPortfolioCategory", x => new { x.BuyerProjectInquireId, x.PortfolioCategoriesId });
                    table.ForeignKey(
                        name: "FK_BuyerProjectInquiryPortfolioCategory_BuyerProjectInquiry_Bu~",
                        column: x => x.BuyerProjectInquireId,
                        principalTable: "BuyerProjectInquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerProjectInquiryPortfolioCategory_PortfolioCategories_Po~",
                        column: x => x.PortfolioCategoriesId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerProjectInquiryPortfolioSubCategory",
                columns: table => new
                {
                    BuyerProjectInquireId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioSubCategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProjectInquiryPortfolioSubCategory", x => new { x.BuyerProjectInquireId, x.PortfolioSubCategoriesId });
                    table.ForeignKey(
                        name: "FK_BuyerProjectInquiryPortfolioSubCategory_BuyerProjectInquiry~",
                        column: x => x.BuyerProjectInquireId,
                        principalTable: "BuyerProjectInquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerProjectInquiryPortfolioSubCategory_PortfolioSubCategor~",
                        column: x => x.PortfolioSubCategoriesId,
                        principalTable: "PortfolioSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProject_BuyerCompanyId",
                table: "BuyerProject",
                column: "BuyerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProject_CreatedById",
                table: "BuyerProject",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProjectInquiry_BuyerProjectId",
                table: "BuyerProjectInquiry",
                column: "BuyerProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProjectInquiryPortfolioCategory_PortfolioCategoriesId",
                table: "BuyerProjectInquiryPortfolioCategory",
                column: "PortfolioCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProjectInquiryPortfolioSubCategory_PortfolioSubCategor~",
                table: "BuyerProjectInquiryPortfolioSubCategory",
                column: "PortfolioSubCategoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerProjectInquiryPortfolioCategory");

            migrationBuilder.DropTable(
                name: "BuyerProjectInquiryPortfolioSubCategory");

            migrationBuilder.DropTable(
                name: "BuyerProjectInquiry");

            migrationBuilder.DropTable(
                name: "BuyerProject");
        }
    }
}
