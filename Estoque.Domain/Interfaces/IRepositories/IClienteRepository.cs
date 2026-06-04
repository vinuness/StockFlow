using Estoque.Domain.Entities.Clientes;

namespace Estoque.Domain.Interfaces.IRepositories
{
    public interface IClienteRepository
    {
        public Task<List<Cliente>> FindAll();
        public Task<Cliente> FindById(int id);
        public Task<Cliente> FindByEmail(string email);
        public Task<Cliente> Save(Cliente cliente);
        public Task Update(Cliente cliente, int id);
        public Task Delete(int id);
        public string GenerateToken(int id);
    }
}
