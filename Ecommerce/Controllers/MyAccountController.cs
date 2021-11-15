using Ecommerce.Data;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Repository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly EcommerceContext _context;

        public MyAccountController(IClienteRepository clienteRepository, EcommerceContext context)
        {
            this._clienteRepository = clienteRepository;
            this._context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = Convert.ToInt32(userId);

            var user = new SqlParameter("IdCliente", id);

            string sql = "EXEC dbo.GetClienteById @IdCliente";
            var list = _context.MyAccounts.FromSqlRaw<MyAccount>(sql, user).ToList();
            Debugger.Break();
            return View(list);

            
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
