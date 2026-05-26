using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Interfaces.IRepositories
{
    public interface ICategoriaRepository
    {
        public Task<List<Categoria>> FindAll();
        public Task<Categoria> FindById(int id);
        public Task<Categoria> Save(Categoria categoria);
        public Task Update(Categoria categoria, int id);
        public Task Delete(int id);
    }
}
