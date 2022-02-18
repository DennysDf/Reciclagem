using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class ItemDepesas
    {
        public ItemDepesas()
        {
            Despesas = new HashSet<Despesas>();
        }
        
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int TipoDepesaId { get; set; }
        public TipoDespesa TipoDespesa { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime DataCad { get; set; }

        public ICollection<Despesas> Despesas { get; set; }
    }
}
