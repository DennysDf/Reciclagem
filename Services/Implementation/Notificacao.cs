using Reciclagem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Services.Implementation
{
    public class Notificacao : INotificacao
    {
        public Notificacao()
        {

        }
        
        public void Success(string mensssge)
        {            
            Servicos.Notificacoes.Push("alertify.success('" + mensssge +"');");
        }

        public void Error()
        {
            Servicos.Notificacoes.Push("alertify.error(Erro ao executar ação.);");
        }

    }
}
