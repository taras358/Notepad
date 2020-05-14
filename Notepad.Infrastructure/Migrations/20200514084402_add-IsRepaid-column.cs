using Microsoft.EntityFrameworkCore.Migrations;

namespace Notepad.Infrastructure.Migrations
{
    public partial class addIsRepaidcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRepaid",
                table: "Debts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRepaid",
                table: "Debts");
        }
    }
}
