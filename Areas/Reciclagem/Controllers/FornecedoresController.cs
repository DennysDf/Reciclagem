using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reciclagem.Areas.Reciclagem.Models.ClientesFornecedores;
using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Services.Interfaces;

namespace Reciclagem.Areas.Reciclagem.Controllers
{
    [Area("Reciclagem")]
    public class FornecedoresController : Controller
    {
        private readonly DBReciclagemContext _context;
        private readonly IUser _user;
        private readonly INotificacao _notify;

        public FornecedoresController(DBReciclagemContext context, IUser user, INotificacao notidy)
        {
            _context = context;
            _user = user;
            _notify = notidy;
        }

        public IActionResult Index()
        {
            ViewData["Tipo"] = 2;

            var cliente = _context.Fornecedores
                .Where(c => c.IsAtivo && c.Tipo == 2)
                .Select(c => new ClientesFornecedoresVM
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Endereco = c.Endereco,
                    Telefone = c.Telefone,
                    Cidade = c.Cidade
                })
                .ToList();

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Index(ClientesFornecedoresVM model)
        {

            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _context.Add(model.Insert(model));
                    _context.SaveChanges();
                    _notify.Success("Fornecedor cadastrado com sucesso.");
                }
                else
                {
                    _context.Update(model.Update(_context, model.Id));
                    _context.SaveChanges();
                    _notify.Success("Fornecedor editado com sucesso.");
                }
            }
            else
            {
                _notify.Error();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AjaxCliente(int id = 0)
        {
            if (id == 0)
                return View(new ClientesFornecedoresVM());
            else
                return View(new ClientesFornecedoresVM(_context, id));
        }

        public IActionResult Delete(int id)
        {
            var fornecedor = _context.Fornecedores.FirstOrDefault(c => c.Id == id);
            fornecedor.IsAtivo = false;
            _context.Update(fornecedor);
            _context.SaveChanges();
            _notify.Success("Forcenedor apagado com sucesso.");

            return RedirectToAction(nameof(Index));
        }
    }
}