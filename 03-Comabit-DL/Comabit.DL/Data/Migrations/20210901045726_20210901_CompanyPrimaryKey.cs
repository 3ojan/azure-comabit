using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Comabit.DL.Migrations
{
    public partial class _20210901_CompanyPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BusinessTaxId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UstId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyGuid",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationPermission",
                columns: table => new
                {
                    ApplicationPermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPermission", x => x.ApplicationPermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address1 = table.Column<string>(type: "text", nullable: true),
                    Address2 = table.Column<string>(type: "text", nullable: true),
                    UstId = table.Column<string>(type: "text", nullable: true),
                    BusinessTaxId = table.Column<string>(type: "text", nullable: true),
                    Confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Guid);
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
                        name: "FK_ApplicationPermissionApplicationRole_ApplicationPermission_~",
                        column: x => x.PermissionsApplicationPermissionId,
                        principalTable: "ApplicationPermission",
                        principalColumn: "ApplicationPermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationPermissionApplicationRole_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyGuid",
                table: "AspNetUsers",
                column: "CompanyGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPermissionApplicationRole_RolesId",
                table: "ApplicationPermissionApplicationRole",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Company_CompanyGuid",
                table: "AspNetUsers",
                column: "CompanyGuid",
                principalTable: "Company",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Company_CompanyGuid",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ApplicationPermissionApplicationRole");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "ApplicationPermission");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyGuid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyGuid",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessTaxId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UstId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
