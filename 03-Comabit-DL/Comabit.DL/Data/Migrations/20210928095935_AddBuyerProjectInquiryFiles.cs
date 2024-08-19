using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class AddBuyerProjectInquiryFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyerProjectInquiryFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerProjectInquiryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProjectInquiryFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerProjectInquiryFile_BuyerProjectInquiry_BuyerProjectInq~",
                        column: x => x.BuyerProjectInquiryId,
                        principalTable: "BuyerProjectInquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerProjectInquiryFileData",
                columns: table => new
                {
                    BuyerProjectInquiryFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileData = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProjectInquiryFileData", x => x.BuyerProjectInquiryFileId);
                    table.ForeignKey(
                        name: "FK_BuyerProjectInquiryFileData_BuyerProjectInquiryFile_BuyerPr~",
                        column: x => x.BuyerProjectInquiryFileId,
                        principalTable: "BuyerProjectInquiryFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProjectInquiryFile_BuyerProjectInquiryId",
                table: "BuyerProjectInquiryFile",
                column: "BuyerProjectInquiryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerProjectInquiryFileData");

            migrationBuilder.DropTable(
                name: "BuyerProjectInquiryFile");
        }
    }
}
