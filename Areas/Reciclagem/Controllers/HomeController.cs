using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reciclagem.Areas.Reciclagem.Models.Home;
using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Models.Login;
using Reciclagem.Services.Interfaces;

namespace Reciclagem.Areas.Reciclagem.Controllers
{
    [Authorize]
    [Area("Reciclagem")]
    public class HomeController : Controller
    {
        private readonly IUser _user;
        private readonly DBReciclagemContext _context;
        private readonly INotificacao _notify;
        private readonly IUpload _upload;
        public IHttpContextAccessor _http { get; set; }

        public HomeController(IUser user, DBReciclagemContext context, INotificacao notify, IUpload upload, IHttpContextAccessor http)
        {
            _user = user;
            _context = context;
            _notify = notify;
            _upload = upload;
            _http = http;
        }

        public IActionResult Index()
        {


            ViewData["QtdComprasChart"] = JsonConvert.SerializeObject(
                 _context.Operacoes
                     .Include(c => c.Fornecedor)
                     .Where(c => c.Status == 2 && c.IsAtivo && c.Tipo == 2)
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
                   .Where(c => c.Status == 2 && c.IsAtivo && c.Tipo == 1)
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


            ViewData["materialMaisVendidos"] = JsonConvert.SerializeObject(
                 _context.MateriaisOperacao
                  .Include(c => c.Operacao)
                  .Include(c => c.Material)
                  .Where(c => c.Operacao.Status == 2 && c.Operacao.IsAtivo && c.Operacao.Tipo == 1)
                  .Select(c => new
                  {
                      Id = c.MaterialId,
                      c.MaterialId,
                      c.Material.Descricao
                  })
                  .GroupBy(c => new { c.Id, c.Descricao })
                  .Select(c => new
                  {
                      Nome = c.Key.Descricao + ": " + c.Count(x => x.MaterialId == c.Key.Id),
                      Valor = c.Count(x => x.MaterialId == c.Key.Id)
                  })
                  .OrderByDescending(c => c.Valor)
                  .ToArray());

            ViewData["materialMaisCompro"] = JsonConvert.SerializeObject(
                _context.MateriaisOperacao
                .Include(c => c.Operacao)
                .Include(c => c.Material)
                .Where(c => c.Operacao.Status == 2 && c.Operacao.IsAtivo && c.Operacao.Tipo == 2)
                .Select(c => new
                {
                    Id = c.MaterialId,
                    c.MaterialId,
                    c.Material.Descricao
                })
                .GroupBy(c => new { c.Id, c.Descricao })
                .Select(c => new
                {
                    Nome = c.Key.Descricao+ ": " + c.Count(x => x.MaterialId == c.Key.Id),
                    Valor = c.Count(x => x.MaterialId == c.Key.Id)
                })
                 .OrderByDescending(c => c.Valor)
                .ToArray());

            ViewData["teste"] = JsonConvert.SerializeObject(
                 _context.MateriaisOperacao
                 .Include(c => c.Operacao)
                 .Where(c => c.Operacao.Status == 2 && c.Operacao.IsAtivo && c.Operacao.Data.ParseDateTime().Year == DateTime.Now.Year)
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
                 .ToList());





            ViewData["QtdVendas"] = _context.Operacoes.Where(c => c.Status == 2 && c.IsAtivo && c.Tipo == 1).Count();

            ViewData["QtdCompras"] = _context.Operacoes.Where(c => c.Status == 2 && c.IsAtivo && c.Tipo == 2).Count();

            ViewData["QtdVendReceb"] = _context.Fornecedores.Where(c => c.IsAtivo).Count(); ;

            ViewData["QtdMaterias"] = _context.Materiais.Where(c => c.IsAtivo).Count();



            return View();
        }

        public IActionResult Perfil()
        {
            var cliente = _context.Usuarios
                .Include(c => c.Empresa)
                .Where(c => c.Id == _user.Id)
                .Select(c => new PerfilVM
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    Endereco = c.Empresa.Endereco,
                    NomeConsultorio = c.Empresa.Nome
                })
                .First();

            var cli = TempData["Filtro"] != null ? JsonConvert.DeserializeObject<PerfilVM>(TempData["Filtro"].ToString()) : cliente;

            return View(cli);
        }

        [HttpPost]
        public async Task<IActionResult> Perfil(PerfilVM model, IFormFile Foto)
        {

            var direitorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\Usuarios");

            var user = _context.Usuarios.First(c => c.Id == _user.Id);

            if (model.SenhaN != null && model.Senha == null)
            {
                TempData["Filtro"] = JsonConvert.SerializeObject(model);
                TempData["MensagemSenhaAtual"] = "Para atualizar a senha digite a senha atual.";
                return RedirectToAction("Perfil");
            }

            var senha = model.Senha != null ? model.Senha : user.Senha;

            if (senha != user.Senha)
            {
                TempData["Filtro"] = JsonConvert.SerializeObject(model);
                TempData["MensagemSenhaAtual"] = "Senha não coincide com a senha atual.";
                return RedirectToAction("Perfil");
            }

            if (Foto != null && user.Foto != null)
            {
                _upload.Delete(direitorio, $"{user.Id}_{user.Foto}");
                user.Foto = null;
            }

            if (Foto != null)
            {
                _upload.Save(direitorio, Foto, _user.Id);
                user.Foto = Path.GetFileName(Foto.FileName);
            }


            user.Nome = model.Nome;
            user.Email = model.Email;
            if (model.SenhaN != null)
                user.Senha = model.SenhaN;
            _context.Update(user);

            var consultorio = _context.Empresas.First(c => c.Id == user.EmpresaId);
            consultorio.Endereco = model.Endereco;
            consultorio.Nome = model.NomeConsultorio;
            _context.Update(consultorio);

            var createCookies = new CreateCookies(_http);
            await createCookies.Autenticar(user);

            _context.SaveChanges();
            _notify.Success("Perfil atualizado com sucesso.");

            return RedirectToAction("Index");
        }

        public bool ValidaSenha(string senha)
        {
            var senhaAtual = _context.Usuarios.First(c => c.Id == _user.Id).Senha;

            return senhaAtual == senha;

        }
    }
}