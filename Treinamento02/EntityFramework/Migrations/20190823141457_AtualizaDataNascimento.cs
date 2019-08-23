using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class AtualizaDataNascimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE ""Cliente"" SET ""DataNascimento"" = '2019-08-23'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
