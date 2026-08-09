using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Pagination;

namespace Estoque.Domain.Interfaces.IRepositories
{
    public interface IProdutoRepository
    {
        public Task<PagedList<Produto>> FindAll(int pageNumber, int pageSize);
        public Task<Produto> FindById(int id);
        public Task<Produto> Save(Produto produto);
        public Task Update(Produto produto);
        public Task Delete(int id);
        public Task<List<ProdutoMaisVendidoDTO>> ProdutosMaisVendidos();
        public Task<ImagemModel> buscarImagem(int id);
    }
}
