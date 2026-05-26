using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _con;

        public ClienteRepository(AppDbContext con)
        {
            _con = con;
        }

        public async Task Delete(int id)
        {
            Cliente cliente = await _con.Clientes.FindAsync(id);
            _con.Clientes.Remove(cliente);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Cliente>> FindAll()    
        {
            List<Cliente> clientes = await _con.Clientes.ToListAsync();
            return clientes;
        }

        public async Task<Cliente> FindById(int id)
        {
            Cliente cliente = await _con.Clientes.FindAsync(id);
            cliente.Endereco = await _con.Enderecos.FindAsync(cliente.EnderecoId);
            return cliente;
        }

        public async Task<Cliente> Save(Cliente cliente)
        {
            _con.Clientes.Add(cliente);
            await _con.SaveChangesAsync();
            return cliente;
        }

        public async Task Update(Cliente cliente, int id)
        {
            cliente.Id = id;
            _con.Clientes.Update(cliente);
            await _con.SaveChangesAsync();
        }

        public async Task<Cliente> Login(string email, string senha)
        {
            Cliente cliente = await _con.Clientes.FirstOrDefaultAsync(c => c.Email == email && c.Senha == senha);
            return cliente;
        }
    }
}
