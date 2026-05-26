using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Entities.Produtos;

namespace Estoque.Infrastructure.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly AppDbContext _con;
        public FornecedorRepository(AppDbContext con)
        {
            _con = con;
        }

        public async Task Delete(int id)
        {
            Fornecedor fornecedor = await _con.Fornecedores.FindAsync(id);
            _con.Fornecedores.Remove(fornecedor);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Fornecedor>> FindAll()
        {
            List<Fornecedor> fornecedores = await _con.Fornecedores.ToListAsync();
            return fornecedores;
        }

        public async Task<Fornecedor> FindById(int id)
        {
            Fornecedor fornecedor = await _con.Fornecedores.FindAsync(id);
            return fornecedor;
        }

        public async Task<Fornecedor> Save(Fornecedor fornecedor)
        {
            _con.Fornecedores.Add(fornecedor);
            await _con.SaveChangesAsync();
            return fornecedor;
        }

        public async Task Update(Fornecedor fornecedor, int id)
        {
            fornecedor.Id = id;
            _con.Fornecedores.Update(fornecedor);
            await _con.SaveChangesAsync();
        }
    }
}
