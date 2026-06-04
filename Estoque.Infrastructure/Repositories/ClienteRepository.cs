using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Infrastructure.Data;
using Estoque.Infrastructure.Utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Estoque.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _con;
        private readonly JWTService _jwt;

        public ClienteRepository(AppDbContext con, JWTService jwt)
        {
            _con = con;
            _jwt = jwt;
        }

        public async Task Delete(int id)
        {
            Cliente cliente = await _con.Clientes.FindAsync(id);
            cliente.Endereco = await _con.Enderecos.FindAsync(cliente.EnderecoId);
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

        public async Task<Cliente> FindByEmail(string email)
        {
            var cliente = await _con.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Email == email);
            
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

        public string GenerateToken(int id)
        {
            return _jwt.GenerateToken(id);
        }
    }
}
