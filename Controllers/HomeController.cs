using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;
using Reciclagem.Models.DBORECICLAGEM.Context;
using Reciclagem.Models.Login;
using Reciclagem.Models.DBORECICLAGEM.Entidades;

namespace Reciclagem.Controllers
{
    public class HomeController : Controller
    {
        public readonly DBReciclagemContext _context;
        public IHttpContextAccessor _http { get; set; }

        public HomeController(DBReciclagemContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM model)
        {

            var user = _context.Usuarios.Include(c => c.Empresa).Where(c => c.Login.Equals(model.Username.ToLower()) && c.Senha == model.Password);

            if (user.Count() == 0)
            {
                TempData["Erro"] = "Error";
                return RedirectToAction("Index");
            }
            else
            {
                var createCookies = new CreateCookies(_http);
                await createCookies.Autenticar(user.First());

                var logado = _context.Usuarios.FirstOrDefault(c => c.Id == user.First().Id);
                logado.LastAcesso = DateTime.Now;
                _context.Update(logado);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "Reciclagem" });
            }

            return RedirectToAction("Index");
        }

        public IActionResult ConfirmarEmail()
        {


            return View();
        }

        [HttpPost]
        public IActionResult ConfirmarEmail(LoginVM model)
        {
            var random = new Random();
            int randomNumber = random.Next(1000, 9999);

            SmtpClient smtp = new SmtpClient("mail.devroyale.com", 8889);
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("postmaster@devroyale.com", "Deusemais@100");
            var msg = new MailMessage();
            msg.To.Add(model.Email);
            msg.From = new MailAddress("postmaster@devroyale.com", "nao-responder@reciclagem.com");

            msg.Subject = "Redefinir senha.";
            msg.Body = $"Seu código de verificação é <b>{randomNumber}</b> <br>Use-o para redefinir sua senha no sistema de gestão para reciclagem.";
            msg.IsBodyHtml = true;
            smtp.Send(msg);

            ViewData["Email"] = model.Email;

            var reset = new ResetSenha()
            {
                Email = model.Email,
                Numero = randomNumber
            };
            _context.Add(reset);
            _context.SaveChanges();

            return RedirectToAction(nameof(RedefinirSenha));
        }

        public IActionResult RedefinirSenha()
        {

            return View();
        }

        public bool ValidaEmail(string email)
        {
            var emails = _context.Usuarios.Where(c => c.IsAtivo && c.Email != null).Select(c => c.Email);

            return emails.Contains(email);

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
