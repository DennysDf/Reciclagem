using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.Login
{
    public class Authenticate
    {
        public int Id { get; set; }        
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }
        public string Consultorio { get; internal set; }
    }
}
