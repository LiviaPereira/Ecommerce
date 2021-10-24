using Ecommerce.Data;
using Ecommerce.Data.Interfaces;
using Ecommerce.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using Ecommerce.Models;
using Ecommerce.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace Ecommerce.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public LoginRegisterController(IClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Cliente c = new Cliente();
                    c.Nome = register.Nome;
                    c.Telefone = register.Telefone;
                    c.Endereco = register.Endereco;
                    c.EnderecoNumero = register.EnderecoNumero;
                    c.EnderecoComplemento = register.EnderecoComplemento;
                    c.Bairro = register.Bairro;
                    c.Cidade = register.Cidade;
                    c.UF = register.UF;
                    c.CEP = register.CEP;
                    c.Email = register.Email;
                    c.Senha = register.Senha;
                    c.Ativo = true;

                    _clienteRepository.InsertCliente(c);

                    return Redirect("/Home/Index");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError(string.Empty, "Erro ao salvar");
            }


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync(Models.Login usuario)
        {
            var result = _clienteRepository.GetClienteByEmail(usuario.Email, usuario.Senha);

            if (result != null && result.Ativo == true)
            {
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Email, result.Email),
                new Claim(ClaimTypes.Name, result.Nome),
                new Claim(ClaimTypes.NameIdentifier, result.IdCliente.ToString())
                //new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = "/Home/Index",
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);                
            }
            else
            {
                ModelState.AddModelError("Email", "Usuário Inválido.");
                return View();
            }
            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Home/Index");
        }

       

    }
}
