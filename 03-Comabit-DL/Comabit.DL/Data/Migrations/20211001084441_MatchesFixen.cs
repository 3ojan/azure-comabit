using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class MatchesFixen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_BuyerProjectInquiry_InquiryId",
                table: "Matchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_Companies_SellerCompanyId",
                table: "Matchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Matchs_MatchId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Matchs_MatchId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matchs",
                table: "Matchs");

            migrationBuilder.RenameTable(
                name: "Matchs",
                newName: "Matches");

            migrationBuilder.RenameIndex(
                name: "IX_Matchs_SellerCompanyId",
                table: "Matches",
                newName: "IX_Matches_SellerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Matchs_InquiryId_SellerCompanyId",
                table: "Matches",
                newName: "IX_Matches_InquiryId_SellerCompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Matches_MatchId",
                table: "Messages",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Matches_MatchId",
                table: "Offers",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_BuyerProjectInquiry_InquiryId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Companies_SellerCompanyId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Matches_MatchId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Matches_MatchId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "Matchs");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_SellerCompanyId",
                table: "Matchs",
                newName: "IX_Matchs_SellerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_InquiryId_SellerCompanyId",
                table: "Matchs",
                newName: "IX_Matchs_InquiryId_SellerCompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matchs",
                table: "Matchs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_BuyerProjectInquiry_InquiryId",
                table: "Matchs",
                column: "InquiryId",
                principalTable: "BuyerProjectInquiry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_Companies_SellerCompanyId",
                table: "Matchs",
                column: "SellerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Matchs_MatchId",
                table: "Messages",
                column: "MatchId",
                principalTable: "Matchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Matchs_MatchId",
                table: "Offers",
                column: "MatchId",
                principalTable: "Matchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
