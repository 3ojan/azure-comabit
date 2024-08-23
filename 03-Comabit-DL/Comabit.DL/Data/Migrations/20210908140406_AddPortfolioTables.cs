using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class AddPortfolioTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioAreas",
                columns: table => new
                {
                    PortfolioAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreas", x => x.PortfolioAreaId);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioAreaCategories",
                columns: table => new
                {
                    PortfolioAreaCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreaCategories", x => x.PortfolioAreaCategoryId);
                    table.ForeignKey(
                        name: "FK_PortfolioAreaCategories_PortfolioAreas_PortfolioAreaId",
                        column: x => x.PortfolioAreaId,
                        principalTable: "PortfolioAreas",
                        principalColumn: "PortfolioAreaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioAreaSubCategories",
                columns: table => new
                {
                    PortfolioAreaSubCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioAreaCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAreaSubCategories", x => x.PortfolioAreaSubCategoryId);
                    table.ForeignKey(
                        name: "FK_PortfolioAreaSubCategories_PortfolioAreaCategories_Portfoli~",
                        column: x => x.PortfolioAreaCategoryId,
                        principalTable: "PortfolioAreaCategories",
                        principalColumn: "PortfolioAreaCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAreaCategories_PortfolioAreaId",
                table: "PortfolioAreaCategories",
                column: "PortfolioAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAreaSubCategories_PortfolioAreaCategoryId",
                table: "PortfolioAreaSubCategories",
                column: "PortfolioAreaCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioAreaSubCategories");

            migrationBuilder.DropTable(
                name: "PortfolioAreaCategories");

            migrationBuilder.DropTable(
                name: "PortfolioAreas");
        }
    }
}
