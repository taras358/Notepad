using Microsoft.EntityFrameworkCore.Migrations;

namespace Notepad.Infrastructure.Migrations
{
    public partial class updatedeleteoption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Debtors_DebtorId",
                table: "Debts");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Debtors_DebtorId",
                table: "Debts",
                column: "DebtorId",
                principalTable: "Debtors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Debtors_DebtorId",
                table: "Debts");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Debtors_DebtorId",
                table: "Debts",
                column: "DebtorId",
                principalTable: "Debtors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
