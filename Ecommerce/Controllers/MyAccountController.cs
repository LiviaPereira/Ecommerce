using Ecommerce.Data.Entities;
using Ecommerce.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public MyAccountController(IClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> InativarCliente()
        {
            var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = Convert.ToInt32(userId);

           var cliente = _clienteRepository.GetClienteByID(id);

            cliente.Ativo = false;           

            _clienteRepository.UpdateCliente(cliente);

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Home/Index");
        }
    }
}
