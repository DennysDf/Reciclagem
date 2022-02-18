using Reciclagem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;   
using System.Security.Claims;


namespace Reciclagem.Services.Implementation
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public User(IHttpContextAccessor accessor )
        {
            _accessor = accessor;
            Id = Int32.Parse(_accessor.HttpContext.User.FindFirstValue("Id"));
            Tipo = Int32.Parse(_accessor.HttpContext.User.FindFirstValue("Tipo"));
            EmpresaId = Int32.Parse(_accessor.HttpContext.User.FindFirstValue("EmpresaId"));
            Nome = _accessor.HttpContext.User.Identity.Name;
            Email = _accessor.HttpContext.User.FindFirstValue("Email");
            Empresa = _accessor.HttpContext.User.FindFirstValue("Empresa");
            Endereco = _accessor.HttpContext.User.FindFirstValue("Endereco");
            Img = _accessor.HttpContext.User.FindFirstValue("Img");
            
        }

        public int Id { get;  }
        public int EmpresaId { get; }
        public int Tipo { get; }
        public string Nome { get;  }
        public string Email { get;  }        
        public string Empresa { get;  }
        public string Endereco { get; }
        public string Img { get; set; }
        //public string Telefone { get; set; }
    }
}
