using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasCasal.Migrations
{
    public partial class CampoEfetivada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SemFim",
                table: "Despesa");

            migrationBuilder.AddColumn<bool>(
                name: "Efetivada",
                table: "Transacao",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fim",
                table: "Despesa",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "CodigoConta",
                table: "Conta",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CodigoAgencia",
                table: "Conta",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Efetivada",
                table: "Transacao");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fim",
                table: "Despesa",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SemFim",
                table: "Despesa",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CodigoConta",
                table: "Conta",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CodigoAgencia",
                table: "Conta",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
