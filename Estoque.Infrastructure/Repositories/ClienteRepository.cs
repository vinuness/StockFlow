using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;
using Estoque.Infrastructure.Data;
using Estoque.Infrastructure.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _con;
        private readonly JWTService _jwt;
        private readonly IEmailSender _email;

        public ClienteRepository(AppDbContext con, JWTService jwt, IEmailSender email)
        {
            _con = con;
            _jwt = jwt;
            _email = email;
        }

        public async Task Delete(int id)
        {
            var cliente = await _con.Clientes
                .Include(c => c.Enderecos)
                .Include(c => c.Pedidos)
                .FirstOrDefaultAsync(c => c.Id == id);

            _con.Clientes.Remove(cliente);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Cliente>> FindAll()    
        {
            List<Cliente> clientes = await _con.Clientes
                .Include(c => c.Enderecos)
                .Include(c => c.Pedidos)
                .ToListAsync();

            return clientes;
        }

        public async Task<Cliente> FindById(int id)
        {
            var cliente = await _con.Clientes
                .Include(c => c.Enderecos)
                .Include(c => c.Pedidos)
                .FirstOrDefaultAsync(c => c.Id == id);

            return cliente;
        }

        public async Task<Cliente> FindByEmail(string email)
        {
            var cliente = await _con.Clientes
                .Include(c => c.Enderecos)
                .Include(c => c.Pedidos)
                .FirstOrDefaultAsync(c => c.Email == email);
            
            return cliente;
        }

        public async Task<Cliente> Save(Cliente cliente)
        {
            cliente.Roles = "Cliente";
            _con.Clientes.Add(cliente);
            await _con.SaveChangesAsync();
            _email.SendEmail(cliente);
            return cliente;
        }

        public async Task Update(Cliente cliente, int id)
        {
            cliente.Id = id;
            _con.Clientes.Update(cliente);
            await _con.SaveChangesAsync();
        }

        public string GenerateToken(Cliente cliente)
        {
            return _jwt.GenerateToken(cliente);
        }
    }
}
