using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Comabit.DL.Migrations
{
    public partial class RemovePermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationPermissionApplicationRole");

            migrationBuilder.DropTable(
                name: "Permissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ApplicationPermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ApplicationPermissionId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPermissionApplicationRole",
                columns: table => new
                {
                    PermissionsApplicationPermissionId = table.Column<int>(type: "integer", nullable: false),
                    RolesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPermissionApplicationRole", x => new { x.PermissionsApplicationPermissionId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ApplicationPermissionApplicationRole_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationPermissionApplicationRole_Permissions_Permission~",
                        column: x => x.PermissionsApplicationPermissionId,
                        principalTable: "Permissions",
                        principalColumn: "ApplicationPermissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPermissionApplicationRole_RolesId",
                table: "ApplicationPermissionApplicationRole",
                column: "RolesId");
        }
    }
}
