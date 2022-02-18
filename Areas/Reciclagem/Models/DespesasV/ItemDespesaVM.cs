using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reciclagem.Migrations;
using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Areas.Reciclagem.Models.DespesaV
{
    public class ItemDespesaVM
    {
        public ItemDespesaVM()
        {

        }

        public ItemDespesaVM(DBReciclagemContext _context)
        {
            TipoDespesas = new SelectList(_context.TiposDespesas.Where(c => c.IsAtivo).Select(c => new { Id = c.Id, Descricao = c.Descricao }), "Id", "Descricao");
        }

        public ItemDespesaVM(DBReciclagemContext _context, int id)
        {
            TipoDespesas = new SelectList(_context.TiposDespesas.Where(c => c.IsAtivo).Select(c => new { Id = c.Id, Descricao = c.Descricao }), "Id", "Descricao");

            var despesas = _context.ItemDespesas.FirstOrDefault(c => c.Id == id);
            this.TipoId = despesas.TipoDepesaId;
            this.Id = despesas.Id;
            Descricao = despesas.Descricao;
        }

        public ItemDespesaVM(DBReciclagemContext _context, bool verifica)
        {
            Itens = new SelectList(_context.ItemDespesas.Include(c=> c.TipoDespesa).Where(c => c.IsAtivo && c.TipoDespesa.IsAtivo).Select(c => new { c.Id, c.Descricao }),"Id","Descricao" );
        }
        
        public ItemDespesaVM(DBReciclagemContext _context,int id, bool verifica)
        {
            Itens = new SelectList(_context.ItemDespesas.Include(c => c.TipoDespesa).Where(c => c.IsAtivo && c.TipoDespesa.IsAtivo).Select(c => new { c.Id, c.Descricao }), "Id", "Descricao");

            var despesa = _context.Despesas.First(c => c.Id == id);
            this.ItensId = despesa.ItemDespesaId;
            this.Valor = despesa.Valor.ToString();
            this.Data = despesa.Data;
            this.Obs = despesa.Descricao;

        }

        public int Id { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Descricao { get; set; }
        public string TipoDespesa { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int TipoId { get; set; }
        public SelectList TipoDespesas { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int ItensId { get; set; }
        public SelectList Itens { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Data { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Valor { get; set; }
        public string Obs { get; set; }
        public string Item { get; internal set; }


        public int Mes { get; set; }
        public int Ano { get; set; }

        public ItemDepesas Insert(ItemDespesaVM model)
        {
            return new ItemDepesas
            {
               Descricao  = this.Descricao,
               TipoDepesaId = this.TipoId
            };
        }

        public ItemDepesas Update(DBReciclagemContext _context, int id)
        {
            var itemDespesas = _context.ItemDespesas.FirstOrDefault(c => c.Id == id);
            itemDespesas.Descricao = this.Descricao;
            itemDespesas.TipoDepesaId = this.TipoId;
            return itemDespesas;
        }

        public Despesas InsertD(ItemDespesaVM model, int id)
        {
            return new Despesas()
            {
                Data = this.Data,
                ItemDespesaId = this.ItensId,
                Descricao = this.Obs,
                UsuarioId = id,
                Valor = decimal.Parse(Valor.Replace(".", "").Replace(",", "."))
        };
        }

        public Despesas UpdateD(DBReciclagemContext _context, int id)
        {
            var despesa = _context.Despesas.FirstOrDefault(c => c.Id == id);
            despesa.Descricao = this.Descricao;
            despesa.Data = this.Data;
            despesa.ItemDespesaId = this.ItensId;
            despesa.Valor = decimal.Parse(Valor.Replace(".", "").Replace(",", "."));
            return despesa;
        }
    }
}
