using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reciclagem.Areas.Reciclagem.Models.DespesaV;
using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Services.Interfaces;

namespace Reciclagem.Areas.Reciclagem.Controllers
{
    [Authorize]
    [Area("Reciclagem")]
    public class DespesasController : Controller
    {
        private readonly DBReciclagemContext _context;
        private readonly IUser _user;
        private readonly INotificacao _notify;

        public DespesasController(DBReciclagemContext context, IUser user, INotificacao notify)
        {
            _context = context;
            _user = user;
            _notify = notify;
        }

        public IActionResult Cadastrar()
        {

            var itens = _context.ItemDespesas
                .Include(c => c.TipoDespesa)
                .Where(c => c.IsAtivo)
                .Select(c => new ItemDespesaVM
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    TipoDespesa = c.TipoDespesa.Descricao
                })
                .ToList();

            return View(itens);
        }

        [HttpPost]
        public IActionResult Cadastrar(ItemDespesaVM model)
        {
          
            if (model.Id == 0)
            {
                _context.Add(model.Insert(model));
                _context.SaveChanges();
                _notify.Success("Item cadastrado com sucesso.");
            }
            else
            {
                _context.Update(model.Update(_context, model.Id));
                _context.SaveChanges();
                _notify.Success("Item editado com sucesso.");
            }

            return RedirectToAction(nameof(Cadastrar));
        }

        public IActionResult AjaxCadastrar(int id = 0)
        {
            if (id == 0)
                return View(new ItemDespesaVM(_context));
            else
                return View(new ItemDespesaVM(_context, id));
        }

        public IActionResult Delete(int id)
        {
            var despesas = _context.ItemDespesas.FirstOrDefault(c => c.Id == id);
            despesas.IsAtivo = false;
            _context.Update(despesas);
            _context.SaveChanges();
            _notify.Success("Item apagado com sucesso.");

            return RedirectToAction(nameof(Cadastrar));
        }


        public IActionResult Index(int mes = 0, int ano = 0)
        {
            ViewData["Mes"] = mes != 0 ? mes:  DateTime.Now.Month;
            ViewData["Ano"] = ano != 0 ? ano: DateTime.Now.Year;

            return View();
        }

        [HttpPost]
        public IActionResult Index(ItemDespesaVM model)
        {

            if (model.Id == 0)
            {
                _context.Add(model.InsertD(model, _user.Id));
                _context.SaveChanges();
                _notify.Success("Despesa cadastrada com sucesso.");
            }
            else
            {
                _context.Update(model.UpdateD(_context,model.Id));
                _context.SaveChanges();
                _notify.Success("Despesa atualizada com sucesso.");
            }

            return RedirectToAction("Index", new { mes = model.Mes, ano = model.Ano });
        }

        public IActionResult AjaxDespesas(int id)
        {   
            if (id == 0)
                return View(new ItemDespesaVM(_context,true));
            else
                return View(new ItemDespesaVM(_context, id,true));
        }

        public IActionResult AjaxTable(int mes, int ano)
        {
            var mesS = mes < 10 ? "0" + mes.ToString() : mes.ToString();
            ViewData["Ano"] = ano;
            ViewData["Mes"] = mes;
            var dataInicial = $"01/{mesS}/{ano}";
            var dataFinal = dataInicial.ParseDateTime().AddMonths(1).AddDays(-1);

            if (dataInicial.ParseDateTime().AddMonths(1) >= $"01/{DateTime.Now.Month}/{DateTime.Now.Year}".ParseDateTime().AddMonths(1))
            {
                ViewData["Disabled"] = "disabled";
            }

            ViewData["Titulo"] = $"{ dataInicial} à {dataFinal.ToShortMinDate()}";

            ViewData["MesAnt"] = mes == 1 ? 12 : mes - 1;
            ViewData["AnoAnt"] = mes == 1 ? ano - 1 : ano;

            ViewData["MesAdd"] = mes == 12 ? 1 : mes + 1;
            ViewData["AnoAdd"] = mes == 12 ? ano + 1 : ano;

            var listaDespesa = _context.Despesas
                .Include(c => c.ItemDespesa)
                    .ThenInclude(c => c.TipoDespesa)
                .Where(c => c.Data.ParseDateTime() >= dataInicial.ParseDateTime() && c.Data.ParseDateTime() <= dataFinal)
                .Select(c => new ItemDespesaVM
                { 
                    Id = c.Id,
                    Data = c.Data,
                    Valor = c.Valor.ToString(),
                    Descricao =  c.Descricao,
                    Item = c.ItemDespesa.Descricao,
                    TipoDespesa = c.ItemDespesa.TipoDespesa.Descricao
                })
                .ToList();

            return View(listaDespesa);
        }

        public IActionResult DeleteDespesa(int id)
        {
            var material = _context.Despesas.FirstOrDefault(c => c.Id == id);
            _context.Remove(material);
            _context.SaveChanges();
            _notify.Success("Despesa apagada com sucesso.");

            return RedirectToAction("Index");
        }

    }
}