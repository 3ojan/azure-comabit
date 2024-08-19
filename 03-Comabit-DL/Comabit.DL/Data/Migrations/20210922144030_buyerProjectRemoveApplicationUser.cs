using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class buyerProjectRemoveApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyerProject_AspNetUsers_CreatedByUserId",
                table: "BuyerProject");

            migrationBuilder.DropIndex(
                name: "IX_BuyerProject_CreatedByUserId",
                table: "BuyerProject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BuyerProject_CreatedByUserId",
                table: "BuyerProject",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyerProject_AspNetUsers_CreatedByUserId",
                table: "BuyerProject",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
