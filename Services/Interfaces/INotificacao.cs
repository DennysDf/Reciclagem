using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Services.Interfaces
{
    public interface INotificacao
    {
        void Success(string message);
        void Error();
    }
}
