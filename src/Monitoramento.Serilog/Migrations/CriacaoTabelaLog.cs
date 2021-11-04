using FluentMigrator;

namespace Monitoramento.Serilog.Migrations
{
    [Migration(1, "Criação da tabela de Log")]
    public class CriacaoTabelaLog : Migration
    {
        public override void Up()
        {
            Create.Table("Log")
                .WithColumn("Id").AsInt64().PrimaryKey("PK_Log").Identity()
                .WithColumn("DataHora").AsDateTime().NotNullable()
                .WithColumn("Tipo").AsString(30).NotNullable()
                .WithColumn("UsuarioId").AsString(30).Nullable()
                .WithColumn("Descricao").AsString(int.MaxValue).Nullable()
                .WithColumn("Excecao").AsString(int.MaxValue).Nullable()
                .WithColumn("Aplicacao").AsString(100).Nullable()
                .WithColumn("RequisicaoHttp").AsString(int.MaxValue).Nullable()
                .WithColumn("RespostaHttp").AsString(int.MaxValue).Nullable()
                .WithColumn("Acao").AsString(400).Nullable()
                .WithColumn("Detalhe").AsString(int.MaxValue).Nullable()
                .WithColumn("Versao").AsString(30).Nullable();
        }

        public override void Down()
        {
            Delete.Table("Log");
        }
    }
}