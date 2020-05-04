using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notepad.Infrastructure.Migrations
{
    public partial class adduserprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedTax",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PartialTax",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Saving",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    ProfileImage = table.Column<string>(nullable: true),
                    FixedTax = table.Column<double>(nullable: false),
                    PartialTax = table.Column<double>(nullable: false),
                    Saving = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.AddColumn<double>(
                name: "FixedTax",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PartialTax",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Saving",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
