using Microsoft.AspNetCore.Mvc.Rendering;
using Reciclagem.Models.DBORECICLAGEM.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Areas.Reciclagem.Models.Relatorios
{
    public class RelatoriosVM
    {
        public RelatoriosVM()
        {

        }

        public RelatoriosVM(int tipo, DBReciclagemContext _context)
        {
            Tipo = tipo;

           ListCliFor = new SelectList(_context.Fornecedores
                .Where(c => c.IsAtivo && c.Tipo == tipo)
                .Select(c => new
                {
                    c.Id,
                    c.Nome
                })
                .ToList(),"Id", "Nome");

        }

        public int Tipo { get; set; }
        [MaxLength(10)]
        public string Inicio { get; set; }
        [MaxLength(10)]
        public string Final { get; set; }
        public int ClienteFornecedorId { get; set; }
        public SelectList ListCliFor { get; set; }
        public int Id { get;  set; }
        public string Nome { get;  set; }
        public string Data { get;  set; }
        public int Qtd { get;  set; }
        public decimal Valor { get; set; }
        public DateTime DataOperacao { get; internal set; }
    }
}
