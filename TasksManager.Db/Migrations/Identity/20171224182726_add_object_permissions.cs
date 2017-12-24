using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TasksManager.Db.Migrations.Identity
{
    public partial class add_object_permissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObjectId = table.Column<int>(nullable: false),
                    ObjectType = table.Column<string>(nullable: false),
                    PermissionTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjectPermissions_PermissionType_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "PermissionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObjectPermissions_PermissionTypeId",
                table: "ObjectPermissions",
                column: "PermissionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectPermissions");

            migrationBuilder.DropTable(
                name: "PermissionType");
        }
    }
}
