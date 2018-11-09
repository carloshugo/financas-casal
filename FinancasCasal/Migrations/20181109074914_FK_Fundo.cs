using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasCasal.Migrations
{
    public partial class FK_Fundo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fundo_Pessoa_DonoId",
                table: "Fundo");

            migrationBuilder.DropIndex(
                name: "IX_Fundo_DonoId",
                table: "Fundo");

            migrationBuilder.DropColumn(
                name: "DonoId",
                table: "Fundo");

            migrationBuilder.AddColumn<int>(
                name: "PessoaId",
                table: "Fundo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fundo_PessoaId",
                table: "Fundo",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fundo_Pessoa_PessoaId",
                table: "Fundo",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fundo_Pessoa_PessoaId",
                table: "Fundo");

            migrationBuilder.DropIndex(
                name: "IX_Fundo_PessoaId",
                table: "Fundo");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Fundo");

            migrationBuilder.AddColumn<int>(
                name: "DonoId",
                table: "Fundo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fundo_DonoId",
                table: "Fundo",
                column: "DonoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fundo_Pessoa_DonoId",
                table: "Fundo",
                column: "DonoId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
