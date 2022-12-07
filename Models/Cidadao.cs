using Dapper.Contrib.Extensions;

namespace dapperOmni.Models
{
    [Table("CIDADAO")]
    public class Cidadao
    {
        [Key]
        public int IdCidadao { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CEP { get; set; }
        public string CPF { get; set; }
        public string TituloEleitor { get; set; }
        public string ZonaEleitor { get; set; }
        public string Secao { get; set; }
        public int ptCidadao { get; set; }
    }
}