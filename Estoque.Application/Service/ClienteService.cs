using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;

namespace Estoque.Application.Service
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Cliente>> FindAll()
        {
            List<Cliente> clientes = await _repo.FindAll();
            return clientes;
        }

        public async Task<Cliente> FindById(int id)
        {
            Cliente cliente = await _repo.FindById(id);
            return cliente;
        }

        public async Task<Cliente> Login(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> Save(Cliente cliente)
        {
            await _repo.Save(cliente);
            return cliente;
        }

        public async Task Update(Cliente cliente, int id)
        {
            await _repo.Update(cliente, id);
        }

        public async Task<Cliente> Login(string email, string senha)
        {
            var cliente = await _repo.Login(email, senha);
            if (cliente != null && cliente.Senha == senha)
            {
                return cliente;
            }
            return null;
        }
    }
}
