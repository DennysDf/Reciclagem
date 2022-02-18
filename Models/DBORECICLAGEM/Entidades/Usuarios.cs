using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class Usuarios
    {
        public Usuarios()
        {
            Operacoes = new HashSet<Operacoes>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public Empresas Empresa { get; set; }
        public int EmpresaId { get; set; }
        public string Foto { get; set; }
        public int Tipo { get; set; }
        public DateTime DataCad { get; set; }
        public DateTime? LastAcesso { get; set; }
        public bool IsAtivo { get; set; }

        public ICollection<Operacoes> Operacoes { get; set; }
        public ICollection<Despesas> Despesas { get; set; }
    }
}
