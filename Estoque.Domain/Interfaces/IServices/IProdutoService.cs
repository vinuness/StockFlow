using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Interfaces.IServices
{
    public interface IProdutoService
    {
        public Task<List<Produto>> FindAll();
        public Task<Produto> FindById(int id);
        public Task<Produto> Save(Produto produto);
        public Task Update(Produto produto, int id);
        public Task Delete(int id);
        public Task<List<Produto>> ListarProdutosCarrinho();
        public Task AddCarrinho(int id);
        public Task RemoverCarrinho(int id);
    }
}
