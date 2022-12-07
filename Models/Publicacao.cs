using System;
using Dapper.Contrib.Extensions;

namespace dapperOmni.Models
{
    [Table("PUBLICACOES")]
    public class Publicacao
    {
        [Key]
        public int IdPublicacao { get; set; }
        public int IdTipoPublicacao { get; set; }
        public string ttPublicacao { get; set; }
        public string txPublicacao { get; set; }
        public string inPublicacao { get; set; }
        public DateTime dtInicioExibicao { get; set; }
        public DateTime dtFimExibicao { get; set; }
        public int snPublicacao { get; set; }
        public int prPublicacao { get; set; }
    }
}