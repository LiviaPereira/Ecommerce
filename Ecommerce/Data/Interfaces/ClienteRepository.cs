using Ecommerce.Data.Entities;
using Ecommerce.Data.Repository;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data.Interfaces
{
    public class ClienteRepository : IClienteRepository, IDisposable
    {
        private EcommerceContext context;

        public ClienteRepository(EcommerceContext context)
        {
            this.context = context;
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return context.Cliente.ToList();
        }

        public Cliente GetClienteByID(int id)
        {
            return context.Cliente.Find(id);
        }

        public Cliente GetClienteByEmail(string email, string senha)
        {
            return context.Cliente.FirstOrDefault(x => x.Email == email && x.Senha == senha);
        }

        public void InsertCliente(Cliente cliente)
        {
            context.Cliente.Add(cliente);
            context.SaveChanges();
        }

        public void DeleteCliente(int clienteID)
        {
            Cliente cliente = context.Cliente.Find(clienteID);
            context.Cliente.Remove(cliente);
        }

        public void UpdateCliente(Cliente cliente)
        {
            context.Entry(cliente).State = EntityState.Modified;
            context.SaveChanges();
        }        

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
