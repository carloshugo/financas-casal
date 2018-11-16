using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasCasal.Migrations
{
    public partial class FK_ContaId_Fundo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContaId",
                table: "Fundo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fundo_ContaId",
                table: "Fundo",
                column: "ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fundo_Conta_ContaId",
                table: "Fundo",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fundo_Conta_ContaId",
                table: "Fundo");

            migrationBuilder.DropIndex(
                name: "IX_Fundo_ContaId",
                table: "Fundo");

            migrationBuilder.DropColumn(
                name: "ContaId",
                table: "Fundo");
        }
    }
}
