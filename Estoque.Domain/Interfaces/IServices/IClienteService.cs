using Estoque.Domain.Entities.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Domain.Interfaces.IServices
{
    public interface IClienteService
    {
        public Task<List<Cliente>> FindAll();
        public Task<Cliente> FindById(int id);
        public Task<Cliente> FindByEmail(string email);
        public Task<Cliente> Save(Cliente cliente);
        public Task Update(Cliente cliente, int id);
        public Task Delete(int id);
        public Task<Cliente> Login(string email, string senha);
    }
}
