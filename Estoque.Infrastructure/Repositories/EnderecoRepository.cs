using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infra.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly AppDbContext _context;

        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Endereco>> FindAll()
        {
            return await _context.Enderecos.ToListAsync();
        }

        public async Task<Endereco> FindById(int id)
        {
            return await _context.Enderecos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(string email, EnderecoDTO dto)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Email == email);

            if (cliente == null) 
                throw new Exception("Cliente não encontrado");

            var endereco = new Endereco
            {
                Rua = dto.Rua,
                Numero = dto.Numero,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Cep = dto.Cep
            };
            cliente.Enderecos.Add(endereco);

            _context.Enderecos.Add(endereco);

            _context.Clientes.Update(cliente);

            await _context.SaveChangesAsync();

        }

        public async Task Update(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var endereco = await FindById(id);
            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
                await _context.SaveChangesAsync();
            }
        }
    }
}