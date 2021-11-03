using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monitoramento.Infra.Data.Migrations
{
    public partial class Teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Excecao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aplicacao = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    RequestHttp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseHttp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acao = table.Column<string>(type: "nvarchar(400)", nullable: true),
                    Detalhe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Versao = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");
        }
    }
}
