using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class TipoDespesa
    {
        public TipoDespesa()
        {
            ItemDespesa = new HashSet<ItemDepesas>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Obs { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime DataCad { get; set; }
        public int EmpresaId { get; set; }
        public Empresas Empresa { get; set; }

        public ICollection<ItemDepesas> ItemDespesa { get; set; }
    }
}
