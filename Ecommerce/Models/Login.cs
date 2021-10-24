using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Login
    {
        //[Key]
        //public int IdCliente { get; set; }
        //[Required(ErrorMessage = "Informe o seu email")]
        //[RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
