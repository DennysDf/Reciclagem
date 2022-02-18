using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class MateriaisOperacao
    {
        public int Id { get; set; }
        public Operacoes Operacao { get; set; }
        public int OperacaoId { get; set; }
        public Materiais Material { get; set; }
        public int MaterialId { get; set; }
        public decimal Peso { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCad { get; set; }
    }
}
