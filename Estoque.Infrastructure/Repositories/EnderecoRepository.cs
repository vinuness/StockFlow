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

        public async Task Save(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
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