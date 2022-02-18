using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class Operacoes
    {
        public Operacoes()
        {
            MateriaisOperacao = new HashSet<MateriaisOperacao>();
        }

        public int Id { get; set; }
        public Usuarios Usuario { get; set; }
        public int UsuarioId { get; set; }
        public Fornecedores Fornecedor { get; set; }
        public int FornecedorId { get; set; }
        public int? Status { get; set; }
        public int Tipo { get; set; }
        public string Data { get; set; }
        public DateTime DataCad { get; set; }
        public bool IsAtivo { get; set; }

        public ICollection<MateriaisOperacao> MateriaisOperacao { get; set; }
    }
}
