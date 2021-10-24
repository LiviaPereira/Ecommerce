using Ecommerce.Data;
using Ecommerce.Data.Repository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
    {

        private readonly EcommerceContext _context;

        public ProductsController(EcommerceContext context)
        {
            _context = context;
        }

        public IActionResult ProductListAsync()
        {
            //var produtos = _context.Produto.ToList();
            //return View(produtos);

            
            string sql = "EXEC [GetProdutosAtivos]";
            var list = _context.Produto.FromSqlRaw<Produto>(sql).ToList();
            Debugger.Break();
            return View(list);
            


        }
    }
}
