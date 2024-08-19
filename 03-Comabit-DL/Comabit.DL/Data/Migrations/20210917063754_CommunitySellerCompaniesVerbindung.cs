using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class CommunitySellerCompaniesVerbindung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Companies_SellerCompanyId",
                table: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Communities_SellerCompanyId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "SellerCompanyId",
                table: "Communities");

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

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySellerCompany_SellerCompaniesId",
                table: "CommunitySellerCompany",
                column: "SellerCompaniesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunitySellerCompany");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerCompanyId",
                table: "Communities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Communities_SellerCompanyId",
                table: "Communities",
                column: "SellerCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Companies_SellerCompanyId",
                table: "Communities",
                column: "SellerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
