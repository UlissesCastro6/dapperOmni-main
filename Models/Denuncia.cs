using Dapper.Contrib.Extensions;
using System;

namespace dapperOmni.Models
{
    [Table("DENUNCIA")]
    public class Denuncia
    {
        [Key]
        public int IdDenuncia { get; set; }
        public int IdCidadao { get; set; }
        public int IdTipoDenuncia { get; set; }
        public int IdStatus { get; set; }
        public DateTime DataDenuncia { get; set; }
        public string DescricaoDenuncia { get; set; }
        public string LocalDenuncia { get; set; }
        public int pontosPrioridade { get; set; }
    }
}