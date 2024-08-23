using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class InquirySellerExclusions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InquirySellerExclusions",
                columns: table => new
                {
                    InquiryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquirySellerExclusions", x => new { x.InquiryId, x.SellerId });
                    table.ForeignKey(
                        name: "FK_InquirySellerExclusions_Companies_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquirySellerExclusions_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InquirySellerExclusions_InquiryId_SellerId",
                table: "InquirySellerExclusions",
                columns: new[] { "InquiryId", "SellerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InquirySellerExclusions_SellerId",
                table: "InquirySellerExclusions",
                column: "SellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InquirySellerExclusions");
        }
    }
}
