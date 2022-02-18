using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Areas.Reciclagem.Models.Operacao
{
    public class OperacaoVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int ClienteFornecedorId { get; set; }
        public SelectList ClientesFornecedores { get; set; }
        [Required(ErrorMessage = Global.Required)]
        [MaxLength(10)]
        public string Data { get; set; }
        public List<OperacoesOpen> OperacoesList { get; set; }
        public int Tipo { get; set; }

        public OperacaoVM()
        {

        }

        public OperacaoVM(DBReciclagemContext _context, int tipo)
        {
            var format = "dd/MM/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;

            ClientesFornecedores = new SelectList(_context.Fornecedores.Where(c => c.IsAtivo && c.Tipo == tipo).Select(c => new { c.Id, c.Nome }).ToList(), "Id", "Nome");

            Tipo = tipo;

            OperacoesList = _context.Operacoes.Include(c => c.Fornecedor).Where(c => c.Status == 1 && c.IsAtivo &&  c.Tipo == tipo).Select(c => new OperacoesOpen { Data = c.Data, Fornecedor = c.Fornecedor.Nome, Id = c.Id, DateOrder = DateTime.ParseExact(c.Data, format, provider)}).OrderByDescending(c => c.DateOrder) .ToList();
        }

        public  Operacoes Insert(int id)
        {
            return new Operacoes()
            {
                FornecedorId = ClienteFornecedorId,
                Data = Data,
                Status = 1,
                UsuarioId = id,
                Tipo = this.Tipo
            };
        }

        public Operacoes Update(DBReciclagemContext _context, int id)
        {
            var operacao = _context.Operacoes.FirstOrDefault(c => c.Id == id);
            operacao.Data = this.Data;
            operacao.FornecedorId = this.ClienteFornecedorId;

            return operacao;
        }

    }

    public class OperacoesOpen
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string Fornecedor { get; set; }
        public List<Materiais> Itens { get; set; }
        public DateTime DateOrder { get; set; }
    }
}
