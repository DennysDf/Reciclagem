using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Reciclagem.Models.DBORECICLAGEM.Entidades;

namespace Reciclagem.Models.Login
{
    public  class CreateCookies
    {
        public IHttpContextAccessor _http { get; set; }

        public CreateCookies(IHttpContextAccessor http)
        {
            _http = http;
        }

        public async Task<Usuarios> Autenticar (Usuarios user)
        {
            var claims = new List<Claim>
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Nome),                    
                    new Claim(ClaimTypes.Role, user.Tipo.ToString()),
                    new Claim("Endereco", user.Empresa.Endereco),
                    new Claim("Empresa", user.Empresa.Nome),
                    new Claim("Email", user.Email),
                    new Claim("Tipo", user.Tipo.ToString()),
                    new Claim("EmpresaId", user.EmpresaId.ToString()),
                    new Claim("Img",  user.Foto != null ? $"{user.Id}_{user.Foto}": "")
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(480),
                AllowRefresh = true
            };

            await _http.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return user;

        }
    }
}
