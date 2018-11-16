using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasCasal.Migrations
{
    public partial class FK_Transacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_ContaId",
                table: "Transacao");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Transacao",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContaId",
                table: "Transacao",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoConta",
                table: "Conta",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoAgencia",
                table: "Conta",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Banco",
                table: "Conta",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_ContaId",
                table: "Transacao",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_ContaId",
                table: "Transacao");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Transacao",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<int>(
                name: "ContaId",
                table: "Transacao",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CodigoConta",
                table: "Conta",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CodigoAgencia",
                table: "Conta",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Banco",
                table: "Conta",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_ContaId",
                table: "Transacao",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
