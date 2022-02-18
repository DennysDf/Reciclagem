using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class Despesas
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Data { get; set; }
        public int ItemDespesaId { get; set; }
        public ItemDepesas ItemDespesa { get; set; }
        public DateTime DataCad { get; set; }
        public bool IsAtivo { get; set; }
        public int UsuarioId { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
