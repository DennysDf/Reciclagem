using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Models.Login
{
    public class LoginVM
    {
        [Required(ErrorMessage = Global.Required)]        
        public string Username { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string SenhaN { get; set; }

        [Remote("ValidaEmail","Home",ErrorMessage =("Não há usuário vinculado a este e-mail."))]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Formato de email inválido.")]
        [Required(ErrorMessage = Global.Required)]
        public string Email { get; set; }
    }
}
