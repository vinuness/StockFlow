using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Interfaces.IRepositories
{
    public interface IProdutoRepository
    {
        public Task<List<Produto>> FindAll();
        public Task<Produto> FindById(int id);
        public Task<Produto> Save(Produto produto);
        public Task Update(Produto produto, int id);
        public Task Delete(int id);
    }
}
