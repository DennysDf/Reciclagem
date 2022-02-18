using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reciclagem.Areas.Reciclagem.Models.Operacao;
using Reciclagem.Areas.Reciclagem.Models.Relatorios;
using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Services.Interfaces;

namespace Reciclagem.Areas.Reciclagem.Controllers
{
    [Area("Reciclagem")]
    public class RelatoriosController : Controller
    {
        private readonly DBReciclagemContext _context;
        private readonly IUser _user;
        private readonly INotificacao _notify;

        public RelatoriosController(DBReciclagemContext context, IUser user, INotificacao notify)
        {
            _context = context;
            _user = user;
            _notify = notify;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Operacao(int id)
        {

            ViewData["Tipo"] = id == 2 ? "Compra" : "Venda";

            ViewData["CliFor"] = id == 2 ? "Fornecedor" : "Cliente";

            ViewData["CliFors"] = id == 2 ? "Fornecedores" : "Clientes";

            return View(new RelatoriosVM(id,_context));
        }

        public IActionResult Resultados(RelatoriosVM model)
        {

            ViewData["Inicial"] = model.Inicio;
            ViewData["Final"] = model.Final;
            ViewData["ClienteFornecedorId"] = model.ClienteFornecedorId;
            ViewData["TipoId"] = model.Tipo;


            ViewData["Tipo"] = model.Tipo == 2 ? "Compra" : "Venda";

            ViewData["Tipo2"] = model.Tipo == 2 ? "Comprados" : "Vendidos";

            ViewData["CliFor"] = model.Tipo == 2 ? "Fornecedor" : "Cliente";

            ViewData["Intervalo"] = $"{model.Inicio} à {model.Final}";

            var filter = _context.Operacoes
                    .Include(c => c.Fornecedor)
                    .Include(c => c.MateriaisOperacao)
                    .Where(c => c.Status == 2 && c.IsAtivo && c.Data.ParseDateTime() >= model.Inicio.ParseDateTime() && c.Data.ParseDateTime() <= model.Final.ParseDateTime() && c.Tipo == model.Tipo);

            if (model.ClienteFornecedorId != 0)
                filter = filter.Where(c => c.FornecedorId == model.ClienteFornecedorId);

            var results = filter.Select(c => new 
            {
                Id = c.Id,
                Nome = c.Fornecedor.Nome,
                Data = c.Data.ToDateMin(),
                DataNum = c.Data,
                Qtd = c.MateriaisOperacao.Count(),
                Valor = _context.MateriaisOperacao.Include(s => s.Material)
                    .Where(s => s.OperacaoId == c.Id)
                    .Select(s => new
                    {
                        Total = Math.Round(s.Peso * s.Valor, 2)
                    })
                    .Sum(s => s.Total)
            })
            .Select(c => new RelatoriosVM
            {
                Id = c.Id,
                Nome = c.Nome,
                Data = c.Data,
                Qtd = c.Qtd,
                Valor = Math.Round(c.Valor,2),
                DataOperacao = c.DataNum.ParseDateTime()
            })
            .OrderBy(c => c.DataOperacao)
            .ToList();

            return View(results);
        }

        public IActionResult Detalhes(int id)
        {

            var operacao = _context.Operacoes.Include(c => c.Fornecedor).First(c => c.Id == id);

            ViewData["Id"] = id;

            ViewData["Tipo"] = operacao.Tipo == 1 ? "Venda" : "Compra";

            ViewData["Operacao"] = operacao.Tipo == 1 ? "Vendidos" : "Comprados";

            ViewData["Fornecedor"] = operacao.Fornecedor.Nome;
            ViewData["Data"] = operacao.Data;

            var itens = _context.MateriaisOperacao
            .Include(c => c.Material)
            .Where(c => c.OperacaoId == id)
            .Select(c => new ItensVM
            {
                Id = c.Id,
                Nome = c.Material.Descricao,
                Peso = c.Peso.ToString(),
                Valor = c.Valor.ToString(),
                Total = Decimal.Multiply(c.Peso, c.Valor).ToString()
            })
            .ToList();

           var a = Math.Round(_context.MateriaisOperacao.Include(c => c.Material)
                    .Where(c => c.OperacaoId == id)
                    .Select(c => new
                    {
                        Total = Math.Round(c.Peso * c.Valor, 2)
                    })
                    .Sum(c => c.Total), 2);

            ViewData["Total"] = a;

            return View(itens);
        }

        public IActionResult Cancelar(int id)
        {
            var operacao = _context.Operacoes.First(c => c.Id == id);
            operacao.IsAtivo = false;
            _context.Update(operacao);
            _context.SaveChanges();
            var tipo = id == 2 ? "Compra" : "Venda";
            _notify.Success($"{tipo} apagada com sucesso.");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Redimentos()
        {
            var anos = Enumerable.Range(2019, (DateTime.Now.Year - 2019) + 1).Select((val, key) => new { key = val, val })
                .OrderByDescending(c => c.val)
                .ToList() ;


            ViewData["Anos"] = new SelectList(anos,"key", "val");

            return View();
        }

        public IActionResult AjaxRedimentos(int ano)
        {
            ViewData["Anos"] = ano;

            var redimentos =
                 _context.MateriaisOperacao
                 .Include(c => c.Operacao)
                 .Where(c => c.Operacao.Status == 2 && c.Operacao.IsAtivo && c.Operacao.Data.ParseDateTime().Year == ano)
                 .Select(c => new
                 {
                     c.Valor,
                     c.Peso,
                     mes = c.Operacao.Data.ParseDateTime().Month,
                     c.Operacao.Tipo
                 })
                 .GroupBy(c => new { c.Tipo })
                 .Select(c => new
                 {
                     name = c.Key.Tipo == 1 ? "Vendas" : "Compras",
                     data = new decimal[]
                     {
                        Decimal.Parse(string.Format("{0:0.00}", c.Where(s => s.mes == 1).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 2).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 3).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 4).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 5).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 6).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 7).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 8).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 9).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 10).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 11).Sum(s => s.Valor * s.Peso))),
                        Decimal.Parse(string.Format("{0:0.00}",c.Where(s => s.mes == 12).Sum(s => s.Valor * s.Peso)))
                     }
                 })
                 .ToList();

            ViewData["Total"] = JsonConvert.SerializeObject(redimentos.Select(c => new { c.name, c.data })
                .GroupBy(c => c.name)
                .Select(c => new
                {
                    c.Key,
                    Valor = c.Sum(x => x.data.Sum())
                }).ToArray());


            ViewData["teste"] = JsonConvert.SerializeObject(redimentos);


            ViewData["QtdComprasChart"] = JsonConvert.SerializeObject(
                 _context.Operacoes
                     .Include(c => c.Fornecedor)
                     .Where(c => c.Status == 2 && c.IsAtivo && c.Tipo == 2 && c.Data.ParseDateTime().Year == ano)
                     .Select(c => new
                     {
                         Qtd = c.FornecedorId,
                         c.FornecedorId,
                         c.Fornecedor.Nome
                     })
                     .GroupBy(c => new { c.Qtd, c.Nome })
                     .Select(c => new
                     {
                         Valor = c.Count(x => x.Qtd == c.Key.Qtd),
                         Nome = c.Key.Nome
                     })
                     .OrderByDescending(c => c.Valor)
                     .ToArray());


            ViewData["QtdVendasChart"] = JsonConvert.SerializeObject(
               _context.Operacoes
                   .Include(c => c.Fornecedor)
                   .Where(c => c.Status == 2 && c.IsAtivo && c.Tipo == 1 && c.Data.ParseDateTime().Year == ano)
                   .Select(c => new
                   {
                       Qtd = c.FornecedorId,
                       c.FornecedorId,
                       c.Fornecedor.Nome
                   })
                   .GroupBy(c => new { c.Qtd, c.Nome })
                   .Select(c => new
                   {
                       Valor = c.Count(x => x.Qtd == c.Key.Qtd),
                       Nome = c.Key.Nome
                   })
                   .OrderByDescending(c => c.Valor)
                   .ToArray());



            return View();
        }

        public IActionResult Print(string Inicial, string Final, int ClienteFornecedorId, int Tipo)
        {
            ViewData["Tipo"] = Tipo == 2 ? "Compra" : "Venda";

            ViewData["Tipo2"] = Tipo == 2 ? "Comprados" : "Vendidos";

            ViewData["CliFor"] = Tipo == 2 ? "Fornecedor" : "Cliente";

            ViewData["Inicial"] = Inicial;
            ViewData["Final"] = Final;

            var filter = _context.Operacoes
                   .Include(c => c.Fornecedor)
                   .Include(c => c.MateriaisOperacao)
                   .Where(c => c.Status == 2 && c.IsAtivo && c.Data.ParseDateTime() >= Inicial.ParseDateTime() && c.Data.ParseDateTime() <= Final.ParseDateTime() && c.Tipo == Tipo);

            if (ClienteFornecedorId != 0)
                filter = filter.Where(c => c.FornecedorId == ClienteFornecedorId);

            var results = filter.Select(c => new
            {
                Id = c.Id,
                Nome = c.Fornecedor.Nome,
                Data = c.Data.ToDateMin(),
                DataNum = c.Data,
                Qtd = c.MateriaisOperacao.Count(),
                Valor = _context.MateriaisOperacao.Include(s => s.Material)
                    .Where(s => s.OperacaoId == c.Id)
                    .Select(s => new
                    {
                        Total = Math.Round(s.Peso * s.Valor, 2)
                    })
                    .Sum(s => s.Total)
            })
            .Select(c => new RelatoriosVM
            {
                Id = c.Id,
                Nome = c.Nome,
                Data = c.Data,
                Qtd = c.Qtd,
                Valor = Math.Round(c.Valor, 2),
                DataOperacao = c.DataNum.ParseDateTime()
            })
            .OrderBy(c => c.DataOperacao)
            .ToList();

            return View(results);
            
        }

        public IActionResult Filtrar(int id)
        {

            return View(new RelatoriosVM(id, _context));
        }

    }
}