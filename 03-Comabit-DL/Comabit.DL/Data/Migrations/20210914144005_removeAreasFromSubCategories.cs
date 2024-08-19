using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class removeAreasFromSubCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalPortfolioAreaCategoryTags");

            migrationBuilder.DropTable(
                name: "PortfolioAreaCategorySellerCompany");

            migrationBuilder.DropTable(
                name: "PortfolioAreaSubCategorySellerCompany");

            migrationBuilder.DropTable(
                name: "PortfolioAreaSubCategories");

            migrationBuilder.DropTable(
                name: "PortfolioAreasCategories");

            migrationBuilder.CreateTable(
                name: "PortfolioCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioCategories_PortfolioAreas_PortfolioAreaId",
                        column: x => x.PortfolioAreaId,
                        principalTable: "PortfolioAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalPortfolioCategoryTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioAreaCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    SellerCompanyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalPortfolioCategoryTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalPortfolioCategoryTags_Companies_SellerCompanyId",
                        column: x => x.SellerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalPortfolioCategoryTags_PortfolioCategories_Portfol~",
                        column: x => x.PortfolioAreaCategoryId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioCategorySellerCompany",
                columns: table => new
                {
                    PortfolioAreaCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCategorySellerCompany", x => new { x.PortfolioAreaCategoriesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_PortfolioCategorySellerCompany_Companies_SellerCompaniesId",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioCategorySellerCompany_PortfolioCategories_Portfoli~",
                        column: x => x.PortfolioAreaCategoriesId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioSubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioSubCategories_PortfolioCategories_PortfolioCategor~",
                        column: x => x.PortfolioCategoryId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioSubCategorySellerCompany",
                columns: table => new
                {
                    PortfolioAreaSubCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioSubCategorySellerCompany", x => new { x.PortfolioAreaSubCategoriesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_PortfolioSubCategorySellerCompany_Companies_SellerCompanies~",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioSubCategorySellerCompany_PortfolioSubCategories_Po~",
                        column: x => x.PortfolioAreaSubCategoriesId,
                        principalTable: "PortfolioSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalPortfolioCategoryTags_PortfolioAreaCategoryId",
                table: "AdditionalPortfolioCategoryTags",
                column: "PortfolioAreaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalPortfolioCategoryTags_SellerCompanyId",
                table: "AdditionalPortfolioCategoryTags",
                column: "SellerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCategories_PortfolioAreaId",
                table: "PortfolioCategories",
                column: "PortfolioAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCategorySellerCompany_SellerCompaniesId",
                table: "PortfolioCategorySellerCompany",
                column: "SellerCompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioSubCategories_PortfolioCategoryId",
                table: "PortfolioSubCategories",
                column: "PortfolioCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioSubCategorySellerCompany_SellerCompaniesId",
                table: "PortfolioSubCategorySellerCompany",
                column: "SellerCompaniesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalPortfolioCategoryTags");

            migrationBuilder.DropTable(
                name: "PortfolioCategorySellerCompany");

            migrationBuilder.DropTable(
                name: "PortfolioSubCategorySellerCompany");

            migrationBuilder.DropTable(
                name: "PortfolioSubCategories");

            migrationBuilder.DropTable(
                name: "PortfolioCategories");

            migrationBuilder.CreateTable(
                name: "PortfolioAreasCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PortfolioAreaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreasCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioAreasCategories_PortfolioAreas_PortfolioAreaId",
                        column: x => x.PortfolioAreaId,
                        principalTable: "PortfolioAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalPortfolioAreaCategoryTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioAreaCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    SellerCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalPortfolioAreaCategoryTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalPortfolioAreaCategoryTags_Companies_SellerCompany~",
                        column: x => x.SellerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalPortfolioAreaCategoryTags_PortfolioAreasCategorie~",
                        column: x => x.PortfolioAreaCategoryId,
                        principalTable: "PortfolioAreasCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioAreaCategorySellerCompany",
                columns: table => new
                {
                    PortfolioAreaCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreaCategorySellerCompany", x => new { x.PortfolioAreaCategoriesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_PortfolioAreaCategorySellerCompany_Companies_SellerCompanie~",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioAreaCategorySellerCompany_PortfolioAreasCategories~",
                        column: x => x.PortfolioAreaCategoriesId,
                        principalTable: "PortfolioAreasCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioAreaSubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PortfolioAreaCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreaSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioAreaSubCategories_PortfolioAreasCategories_Portfol~",
                        column: x => x.PortfolioAreaCategoryId,
                        principalTable: "PortfolioAreasCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioAreaSubCategorySellerCompany",
                columns: table => new
                {
                    PortfolioAreaSubCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerCompaniesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreaSubCategorySellerCompany", x => new { x.PortfolioAreaSubCategoriesId, x.SellerCompaniesId });
                    table.ForeignKey(
                        name: "FK_PortfolioAreaSubCategorySellerCompany_Companies_SellerCompa~",
                        column: x => x.SellerCompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioAreaSubCategorySellerCompany_PortfolioAreaSubCateg~",
                        column: x => x.PortfolioAreaSubCategoriesId,
                        principalTable: "PortfolioAreaSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalPortfolioAreaCategoryTags_PortfolioAreaCategoryId",
                table: "AdditionalPortfolioAreaCategoryTags",
                column: "PortfolioAreaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalPortfolioAreaCategoryTags_SellerCompanyId",
                table: "AdditionalPortfolioAreaCategoryTags",
                column: "SellerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAreaCategorySellerCompany_SellerCompaniesId",
                table: "PortfolioAreaCategorySellerCompany",
                column: "SellerCompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAreasCategories_PortfolioAreaId",
                table: "PortfolioAreasCategories",
                column: "PortfolioAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAreaSubCategories_PortfolioAreaCategoryId",
                table: "PortfolioAreaSubCategories",
                column: "PortfolioAreaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAreaSubCategorySellerCompany_SellerCompaniesId",
                table: "PortfolioAreaSubCategorySellerCompany",
                column: "SellerCompaniesId");
        }
    }
}
