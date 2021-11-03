using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monitoramento.Domain
{
    public sealed class LogModel : EntityBase
    {
        public LogModel()
        {
            
        }

        public DateTime? DataHora { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Tipo { get; set; }


        [Column(TypeName = "nvarchar(30)")]
        public string Usuario { get; set; }

        public string Descricao { get; set; }

        public string Excecao { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Aplicacao { get; set; }

        public string RequestHttp { get; set; }

        public string ResponseHttp { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string Acao { get; set; }

        public string Detalhe { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Versao { get; set; }
    }
}
