using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasCasal.Migrations
{
    public partial class Apelido_Conta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apelido",
                table: "Conta",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apelido",
                table: "Conta");
        }
    }
}
