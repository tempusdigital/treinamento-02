using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class AdicionaOQueFaltava : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Cliente",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "Cliente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CpfOuCnpj",
                table: "Cliente",
                column: "CpfOuCnpj",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_CpfOuCnpj",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Cliente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Cliente",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
