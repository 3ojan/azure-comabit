using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class MatchIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matchs_InquiryId",
                table: "Matchs");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_InquiryId_SellerCompanyId",
                table: "Matchs",
                columns: new[] { "InquiryId", "SellerCompanyId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matchs_InquiryId_SellerCompanyId",
                table: "Matchs");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_InquiryId",
                table: "Matchs",
                column: "InquiryId");
        }
    }
}
