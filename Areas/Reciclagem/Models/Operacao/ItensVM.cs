using Microsoft.AspNetCore.Mvc.Rendering;
using Reciclagem.Models.DBORECICLAGEM.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Areas.Reciclagem.Models.Operacao
{
    public class ItensVM
    {
        public int IdMaterialOperacao { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Valor { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Peso { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int MaterialId { get; set; }
        public SelectList MaterailList { get; set; }
        public string Total { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; internal set; }

        public ItensVM()
        {

        }

        public ItensVM(DBReciclagemContext _context)
        {
            IdMaterialOperacao = 0;

            MaterailList = new SelectList(_context.Materiais.Where(c => c.IsAtivo).Select(c => new { c.Descricao, c.Id }), "Id", "Descricao");
        }
    }
}
