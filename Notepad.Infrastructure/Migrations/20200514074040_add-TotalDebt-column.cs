using Microsoft.EntityFrameworkCore.Migrations;

namespace Notepad.Infrastructure.Migrations
{
    public partial class addTotalDebtcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalDebt",
                table: "Debtors",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDebt",
                table: "Debtors");
        }
    }
}
