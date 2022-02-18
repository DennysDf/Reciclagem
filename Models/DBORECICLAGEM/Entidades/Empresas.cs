using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class Empresas
    {
        public Empresas()
        {
            Usuarios = new HashSet<Usuarios>();
            TipoDespesa = new HashSet<TipoDespesa>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime DataCad { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
        public ICollection<TipoDespesa> TipoDespesa { get; set; }
    }
}
