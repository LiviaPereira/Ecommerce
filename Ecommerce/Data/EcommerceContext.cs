using Ecommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
    public class EcommerceContext:DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options):base(options)
        {

        }
        public DbSet<Cliente> Cliente { get; set; } 
        public DbSet<Ecommerce.Models.Produto> Produto { get; set; } 
    }
}
