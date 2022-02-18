using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.DBORECICLAGEM.Entidades
{
    public class ResetSenha
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Numero { get; set; }
        public DateTime DataHora { get; set; }
        public bool IsAtivo { get; set; }
    }
}
