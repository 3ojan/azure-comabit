using Microsoft.EntityFrameworkCore.Migrations;

namespace Comabit.DL.Migrations
{
    public partial class changebuyerproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyerProject_AspNetUsers_CreatedById",
                table: "BuyerProject");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "BuyerProject",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BuyerProject_CreatedById",
                table: "BuyerProject",
                newName: "IX_BuyerProject_CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyerProject_AspNetUsers_CreatedByUserId",
                table: "BuyerProject",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyerProject_AspNetUsers_CreatedByUserId",
                table: "BuyerProject");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "BuyerProject",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_BuyerProject_CreatedByUserId",
                table: "BuyerProject",
                newName: "IX_BuyerProject_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyerProject_AspNetUsers_CreatedById",
                table: "BuyerProject",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
