using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reciclagem.Areas.Reciclagem.Models.Operacao;

using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Models.DBORECICLAGEM.Entidades;
using Reciclagem.Services.Interfaces;

namespace Reciclagem.Areas.Reciclagem.Controllers
{
    [Area("Reciclagem")]
    public class OperacoesController : Controller
    {
        private readonly DBReciclagemContext _context;
        private readonly IUser _user;
        private readonly INotificacao _notify;

        public OperacoesController(DBReciclagemContext context, IUser user, INotificacao notify)
        {
            _context = context;
            _user = user;
            _notify = notify;
        }

        public IActionResult Index(int tipo)
        {
            return View(new OperacaoVM(_context,tipo));
        }

        [HttpPost]
        public IActionResult Index(OperacaoVM model)
        {
            var tipo = new string[] {"Venda", "Compra" };
            Operacoes operacao = null;
            if (model.Id != 0)
            {
                operacao = model.Update(_context, model.Id);
                _context.Update(operacao);
                _notify.Success($"{tipo[model.Tipo-1]} editada com sucesso.");

            }
            else
            {
                operacao = model.Insert(_user.Id);
                _context.Add(operacao);
                _notify.Success($"{tipo[model.Tipo-1]} cadastrada com sucesso.");
            }

            _context.SaveChanges();

            return RedirectToAction("Itens", new { id = operacao.Id});
        }

        public IActionResult Delete(int id, int tipo)
        {
            var material = _context.Operacoes.FirstOrDefault(c => c.Id == id);
            material.IsAtivo = false;
            _context.Update(material);
            var operacao = new string[] { "Venda", "Compra" };
            _context.SaveChanges();
            _notify.Success($"{operacao[tipo-1]} apagado com sucesso.");

            return RedirectToAction(nameof(Index), new {tipo });
        }

        public IActionResult Itens(int id)
        {
            var operacao = _context.Operacoes.Include(c => c.Fornecedor).First(c => c.Id == id);

            ViewData["Id"] = id;

            ViewData["Tipo"] = operacao.Tipo == 1 ? "Venda" : "Compra";

            ViewData["Operacao"] = operacao.Tipo == 1 ? "Vendidos" : "Comprados";

            ViewData["Fornecedor"] = operacao.Fornecedor.Nome;
            ViewData["Data"] = operacao.Data;


            return View(new ItensVM(_context));
        }
        
        public IActionResult AjaxItem(string peso, string valor, int materailid, int id, int idMaterialOperacao)
        {
            if (materailid != 0 )
            {
                var valorD = decimal.Parse(valor.Replace(".", "").Replace(",", "."));
                var pesoD = decimal.Parse(peso.Replace(".", "").Replace(",", "."));

                if (idMaterialOperacao != 0)
                {
                    var materialOperacao = _context.MateriaisOperacao.First(c => c.Id == idMaterialOperacao);
                    materialOperacao.Peso = pesoD;
                    materialOperacao.Valor = valorD;
                    materialOperacao.MaterialId = materailid;
                    _context.Update(materialOperacao);
                    _context.SaveChanges();
                }
                else
                {
                    var itenOperacao = new MateriaisOperacao()
                    {
                        MaterialId = materailid,
                        OperacaoId = id,
                        Peso = pesoD,
                        Valor = valorD
                    };
                    _context.Add(itenOperacao);
                    _context.SaveChanges();

                }
            }

            var itens = _context.MateriaisOperacao
                    .Include(c => c.Material)
                    .Where(c => c.OperacaoId == id)
                    .Select(c => new ItensVM
                    {
                        Id = c.Id,
                        Nome = c.Material.Descricao,
                        Peso = c.Peso.ToString(),
                        Valor = c.Valor.ToString(),
                        Total = Decimal.Multiply(c.Peso,c.Valor).ToString()
                    })
                    .ToList();

            ViewData["Total"] = Math.Round(_context.MateriaisOperacao
                    .Include(c => c.Material)
                    .Where(c => c.OperacaoId == id)
                    .Select(c => new
                    {
                        Total = Math.Round(c.Peso * c.Valor,2)
                    })
                    .Sum(c => c.Total),2);

            return View(itens);
        }

        public string AjaxItens(int id)
        {
            return JsonConvert.SerializeObject(_context.MateriaisOperacao.First(c => c.Id == id));
        }

        public IActionResult DeleteItem(int id)
        {
            var material = _context.MateriaisOperacao.FirstOrDefault(c => c.Id == id);
            _context.Remove(material);
            _context.SaveChanges();
            _notify.Success("Material apagado com sucesso.");

            return RedirectToAction(nameof(Itens), new { id = material.OperacaoId });
        }

        public IActionResult Finalizar(int id)
        {
            var operacao = _context.Operacoes.First(c => c.Id == id);
            operacao.Status = 2;
            _context.Update(operacao);
            _context.SaveChanges();
            var tipo = operacao.Tipo == 1 ? "Venda" : "Compra";
            _notify.Success($"{tipo} finalizada com sucesso.");

            return RedirectToAction("Index","Home");
        }

    }
}