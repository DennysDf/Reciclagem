using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class Materiais
    {
        public Materiais()
        {
            MateriaisOperacao = new HashSet<MateriaisOperacao>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime DataCad { get; set; }

        public ICollection<MateriaisOperacao> MateriaisOperacao { get; set; }
    }
}
