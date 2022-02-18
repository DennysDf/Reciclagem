using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class Fornecedores
    {
        public Fornecedores()
        {
            Operacoes = new HashSet<Operacoes>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public int Tipo { get; set; }
        public string Telefone { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime DataCad { get; set; }

        public ICollection<Operacoes> Operacoes { get; set; }
    }
}
