using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Areas.Reciclagem.Models.Home
{
    public class PerfilVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Nome { get; set; }
        [Required(ErrorMessage = Global.Required)]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Endereco { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string NomeConsultorio { get; set; }    
        [Remote("ValidaSenha","Home", ErrorMessage = "A senha deve ser idêntica à atual.")]
        public string Senha { get; set; }
        [MinLength(6, ErrorMessage = ("Minímo de 6 caracteres."))]
        public string SenhaN { get; set; }
        [Compare("SenhaN", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirSenha { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Conselho { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Especialidade { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Telefone { get; set; }
    }
}
