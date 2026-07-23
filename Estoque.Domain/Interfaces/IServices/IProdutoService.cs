using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Interfaces.IServices
{
    public interface IProdutoService
    {
        public Task<List<Produto>> FindAll();
        public Task<Produto> FindById(int id);
        public Task<Produto> Save(Produto produto);
        public Task Update(Produto produto);
        public Task Delete(int id);
        public Task<List<ItemCarrinho>> ListarProdutosCarrinho(string email);
        public Task AddCarrinho(string email, int id);
        public Task RemoverCarrinho(string email, int id);
        public Task limparCarrinho(string email);
        public Task<List<ProdutoMaisVendidoDTO>> ProdutosMaisVendidos();
        public Task<ImagemModel> buscarImagem(int id);
    }
}
