using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Areas.Reciclagem.Models.Material
{
    public class MateriaisVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Nome { get; set; }
        

        public MateriaisVM()
        {

        }

        public MateriaisVM(DBReciclagemContext _context, int id)
        {
            var material = _context.Materiais.FirstOrDefault(c => c.Id == id);
            Nome = material.Descricao;
            
        }

        public Materiais Insert(MateriaisVM model)
        {
            return new Materiais
            {
                Descricao = this.Nome
            };
        }

        public Materiais Update(DBReciclagemContext _context, int id)
        {
            var material = _context.Materiais.FirstOrDefault(c => c.Id == id);
            material.Descricao = this.Nome;
            return material;
        }

    }
}
