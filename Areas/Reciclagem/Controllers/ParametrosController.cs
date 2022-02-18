using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Reciclagem.Areas.Reciclagem.Controllers
{
    [Area("Reciclagem")]
    public class ParametrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}