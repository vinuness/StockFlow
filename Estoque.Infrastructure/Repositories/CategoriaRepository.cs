using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Entities.Produtos;

namespace Estoque.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _con;
        public CategoriaRepository(AppDbContext con)
        {
            _con = con;
        }

        public async Task Delete(int id)
        {
            Categoria categoria = await _con.Categorias.FindAsync(id);
            _con.Categorias.Remove(categoria);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Categoria>> FindAll()
        {
            List<Categoria> categorias = await _con.Categorias.ToListAsync();
            return categorias;
        }

        public async Task<Categoria> FindById(int id)
        {
            Categoria categoria = await _con.Categorias.FindAsync(id);
            return categoria;
        }

        public async Task<Categoria> Save(Categoria categoria)
        {
            _con.Categorias.Add(categoria);
            await _con.SaveChangesAsync();
            return categoria;
        }

        public async Task Update(Categoria categoria, int id)
        {
            categoria.Id = id;
            _con.Categorias.Update(categoria);
            await _con.SaveChangesAsync();
        }
    }
}
