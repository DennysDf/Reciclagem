using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Services.Interfaces
{
    public interface IUser
    {
        int Id { get; }
        int Tipo { get;  }
        string Nome { get; }
        string Empresa { get; }
        string Endereco { get;  }
        string Img { get;  }
        int EmpresaId { get; }
       // string Telefone { get; }
    }
}
