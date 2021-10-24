using Ecommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data.Repository
{
    public interface IClienteRepository : IDisposable
    {
        IEnumerable<Cliente> GetClientes();
        Cliente GetClienteByID(int clienteId);
        void InsertCliente(Cliente cliente);
        void DeleteCliente(int clienteID);
        void UpdateCliente(Cliente cliente);
        void Save();
        Cliente GetClienteByEmail(string email, string senha);
    }
}
