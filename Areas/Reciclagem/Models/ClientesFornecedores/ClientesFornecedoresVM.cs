using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Areas.Reciclagem.Models.ClientesFornecedores
{
    public class ClientesFornecedoresVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Nome { get; set; }        
        [Required(ErrorMessage = Global.Required)]
        public string Cidade { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Endereco { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Telefone { get; set; }
        public int Tipo { get; set; }

        public ClientesFornecedoresVM()
        {

        }

        public ClientesFornecedoresVM(DBReciclagemContext _context, int id)
        {
            var clienteFornecedores = _context.Fornecedores.FirstOrDefault(c => c.Id == id);
            Nome = clienteFornecedores.Nome;            
            Endereco = clienteFornecedores.Endereco;            
            Telefone = clienteFornecedores.Telefone;
            Cidade = clienteFornecedores.Cidade;
        }

        public Fornecedores Insert(ClientesFornecedoresVM model)
        {
            return new Fornecedores
            {
                Nome = this.Nome,
                Endereco = this.Endereco,
                Telefone = this.Telefone,
                Cidade = this.Cidade,
                Tipo = this.Tipo
            };
        }

        public Fornecedores Update(DBReciclagemContext _context, int id)
        {
            var clienteFornecedores = _context.Fornecedores.FirstOrDefault(c => c.Id == id);
            clienteFornecedores.Nome = this.Nome;
            clienteFornecedores.Endereco = this.Endereco;
            clienteFornecedores.Telefone = this.Telefone;
            clienteFornecedores.Cidade = this.Cidade;
            return clienteFornecedores;
        }

    }
}
