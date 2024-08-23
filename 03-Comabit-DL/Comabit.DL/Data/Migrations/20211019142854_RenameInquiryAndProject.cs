using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Comabit.DL.Migrations
{
    public partial class RenameInquiryAndProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_BuyerProjectInquiry_BuyerProjectInquiryId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_BuyerProjectInquiry_InquiryId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Companies_SellerCompanyId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "BuyerProjectInquiryPortfolioCategory");

            migrationBuilder.DropTable(
                name: "BuyerProjectInquiryPortfolioSubCategory");

            migrationBuilder.DropTable(
                name: "CommunitySellerCompany");

            migrationBuilder.DropTable(
                name: "PortfolioCategorySellerCompany");

            migrationBuilder.DropTable(
                name: "PortfolioSubCategorySellerCompany");

            migrationBuilder.DropTable(
                name: "BuyerProjectInquiry");

            migrationBuilder.DropTable(
                name: "BuyerProject");

            migrationBuilder.RenameColumn(
                name: "SellerCompanyId",
                table: "Matches",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_SellerCompanyId",
                table: "Matches",
                newName: "IX_Matches_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_InquiryId_SellerCompanyId",
                table: "Matches",
                newName: "IX_Matches_InquiryId_SellerId");

            migrationBuilder.RenameColumn(
                name: "BuyerProjectInquiryId",
                table: "Files",
                newName: "InquiryId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_BuyerProjectInquiryId",
                table: "Files",
                newName: "IX_Files_InquiryId");

            migrationBuilder.CreateTable(
                name: "CommunitySeller",
                columns: table => new
                {
                    CommunitiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunitySeller", x => new { x.CommunitiesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_CommunitySeller_Communities_CommunitiesId",
                        column: x => x.CommunitiesId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunitySeller_Companies_SellerCompaniesId",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioCategorySeller",
                columns: table => new
                {
                    PortfolioCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCategorySeller", x => new { x.PortfolioCategoriesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_PortfolioCategorySeller_Companies_SellerCompaniesId",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioCategorySeller_PortfolioCategories_PortfolioCatego~",
                        column: x => x.PortfolioCategoriesId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioSubCategorySeller",
                columns: table => new
                {
                    PortfolioSubCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioSubCategorySeller", x => new { x.PortfolioSubCategoriesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_PortfolioSubCategorySeller_Companies_SellerCompaniesId",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioSubCategorySeller_PortfolioSubCategories_Portfolio~",
                        column: x => x.PortfolioSubCategoriesId,
                        principalTable: "PortfolioSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    ContactClerk = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InquiryNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Purepose = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                    table.UniqueConstraint("AK_Inquiries_InquiryNumber", x => x.InquiryNumber);
                    table.ForeignKey(
                        name: "FK_Inquiries_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryPortfolioCategory",
                columns: table => new
                {
                    BuyerProjectInquireId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioCategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryPortfolioCategory", x => new { x.BuyerProjectInquireId, x.PortfolioCategoriesId });
                    table.ForeignKey(
                        name: "FK_InquiryPortfolioCategory_Inquiries_BuyerProjectInquireId",
                        column: x => x.BuyerProjectInquireId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryPortfolioCategory_PortfolioCategories_PortfolioCateg~",
                        column: x => x.PortfolioCategoriesId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryPortfolioSubCategory",
                columns: table => new
                {
                    BuyerProjectInquireId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioSubCategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryPortfolioSubCategory", x => new { x.BuyerProjectInquireId, x.PortfolioSubCategoriesId });
                    table.ForeignKey(
                        name: "FK_InquiryPortfolioSubCategory_Inquiries_BuyerProjectInquireId",
                        column: x => x.BuyerProjectInquireId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryPortfolioSubCategory_PortfolioSubCategories_Portfoli~",
                        column: x => x.PortfolioSubCategoriesId,
                        principalTable: "PortfolioSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySeller_SellerCompaniesId",
                table: "CommunitySeller",
                column: "SellerCompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_ProjectId",
                table: "Inquiries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryPortfolioCategory_PortfolioCategoriesId",
                table: "InquiryPortfolioCategory",
                column: "PortfolioCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryPortfolioSubCategory_PortfolioSubCategoriesId",
                table: "InquiryPortfolioSubCategory",
                column: "PortfolioSubCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCategorySeller_SellerCompaniesId",
                table: "PortfolioCategorySeller",
                column: "SellerCompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioSubCategorySeller_SellerCompaniesId",
                table: "PortfolioSubCategorySeller",
                column: "SellerCompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BuyerId",
                table: "Projects",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Inquiries_InquiryId",
                table: "Files",
                column: "InquiryId",
                principalTable: "Inquiries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Companies_SellerId",
                table: "Matches",
                column: "SellerId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Inquiries_InquiryId",
                table: "Matches",
                column: "InquiryId",
                principalTable: "Inquiries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Inquiries_InquiryId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Companies_SellerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Inquiries_InquiryId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "CommunitySeller");

            migrationBuilder.DropTable(
                name: "InquiryPortfolioCategory");

            migrationBuilder.DropTable(
                name: "InquiryPortfolioSubCategory");

            migrationBuilder.DropTable(
                name: "PortfolioCategorySeller");

            migrationBuilder.DropTable(
                name: "PortfolioSubCategorySeller");

            migrationBuilder.DropTable(
                name: "Inquiries");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Matches",
                newName: "SellerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_SellerId",
                table: "Matches",
                newName: "IX_Matches_SellerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_InquiryId_SellerId",
                table: "Matches",
                newName: "IX_Matches_InquiryId_SellerCompanyId");

            migrationBuilder.RenameColumn(
                name: "InquiryId",
                table: "Files",
                newName: "BuyerProjectInquiryId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_InquiryId",
                table: "Files",
                newName: "IX_Files_BuyerProjectInquiryId");

            migrationBuilder.CreateTable(
                name: "BuyerProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerCompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    ContactClerk = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerProject_Companies_BuyerCompanyId",
                        column: x => x.BuyerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunitySellerCompany",
                columns: table => new
                {
                    CommunitiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunitySellerCompany", x => new { x.CommunitiesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_CommunitySellerCompany_Communities_CommunitiesId",
                        column: x => x.CommunitiesId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunitySellerCompany_Companies_SellerCompaniesId",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioCategorySellerCompany",
                columns: table => new
                {
                    PortfolioCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCategorySellerCompany", x => new { x.PortfolioCategoriesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_PortfolioCategorySellerCompany_Companies_SellerCompaniesId",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioCategorySellerCompany_PortfolioCategories_Portfoli~",
                        column: x => x.PortfolioCategoriesId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioSubCategorySellerCompany",
                columns: table => new
                {
                    PortfolioSubCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioSubCategorySellerCompany", x => new { x.PortfolioSubCategoriesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_PortfolioSubCategorySellerCompany_Companies_SellerCompanies~",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioSubCategorySellerCompany_PortfolioSubCategories_Po~",
                        column: x => x.PortfolioSubCategoriesId,
                        principalTable: "PortfolioSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerProjectInquiry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddidtionalTags = table.Column<string>(type: "text", nullable: true),
                    BuyerProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeadlineInfo = table.Column<string>(type: "text", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeliveryInfo = table.Column<string>(type: "text", nullable: true),
                    DeliveryPlace = table.Column<string>(type: "text", nullable: true),
                    InquiryNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Purepose = table.Column<string>(type: "text", nullable: true),
                    Requirements = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySellerCompany_SellerCompaniesId",
                table: "CommunitySellerCompany",
                column: "SellerCompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCategorySellerCompany_SellerCompaniesId",
                table: "PortfolioCategorySellerCompany",
                column: "SellerCompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioSubCategorySellerCompany_SellerCompaniesId",
                table: "PortfolioSubCategorySellerCompany",
                column: "SellerCompaniesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_BuyerProjectInquiry_BuyerProjectInquiryId",
                table: "Files",
                column: "BuyerProjectInquiryId",
                principalTable: "BuyerProjectInquiry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_BuyerProjectInquiry_InquiryId",
                table: "Matches",
                column: "InquiryId",
                principalTable: "BuyerProjectInquiry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Companies_SellerCompanyId",
                table: "Matches",
                column: "SellerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
