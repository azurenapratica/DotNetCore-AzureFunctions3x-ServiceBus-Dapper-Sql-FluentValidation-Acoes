using System;
using Dapper.Contrib.Extensions;

namespace FunctionAppAcoes.Models
{
    [Table("dbo.Acoes")]
    public class Acao
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime? DataReferencia { get; set; }
        public double? Valor { get; set; }
    }
}