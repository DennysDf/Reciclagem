using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reciclagem.Areas.Reciclagem.Models.Material;
using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Services.Interfaces;

namespace Reciclagem.Areas.Reciclagem.Controllers
{
    [Area("Reciclagem")]
    public class MateriaisController : Controller
    {
        private readonly DBReciclagemContext _context;
        private readonly IUser _user;
        private readonly INotificacao _notify;

        public MateriaisController(DBReciclagemContext context, IUser user, INotificacao notidy)
        {
            _context = context;
            _user = user;
            _notify = notidy;
        }

        public IActionResult Index()
        {
            var materiais = _context.Materiais.Where(c => c.IsAtivo).Select(c => new MateriaisVM
            {
                Id = c.Id,
                Nome = c.Descricao           
            }).ToList();

            return View(materiais);
        }

        [HttpPost]
        public IActionResult Index(MateriaisVM model)
        {

            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _context.Add(model.Insert(model));
                    _context.SaveChanges();
                    _notify.Success("Material cadastrado com sucesso.");
                }
                else
                {
                    _context.Update(model.Update(_context, model.Id));
                    _context.SaveChanges();
                    _notify.Success("Material editado com sucesso.");
                }
            }
            else
            {
                _notify.Error();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AjaxMateriais(int id = 0)
        {
            if (id == 0)
                return View(new MateriaisVM());
            else
                return View(new MateriaisVM(_context, id));
        }

        public IActionResult Delete(int id)
        {
            var material = _context.Materiais.FirstOrDefault(c => c.Id == id);
            material.IsAtivo = false;
            _context.Update(material);
            _context.SaveChanges();
            _notify.Success("Material apagado com sucesso.");

            return RedirectToAction(nameof(Index));
        }
    }
}