using Microsoft.EntityFrameworkCore.Migrations;

namespace Notepad.Infrastructure.Migrations
{
    public partial class removeTotalDebtColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDebt",
                table: "Debtors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalDebt",
                table: "Debtors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
