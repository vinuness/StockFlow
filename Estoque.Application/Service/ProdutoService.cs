using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;

namespace Estoque.Application.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repo;
        private readonly ICarrinhoRepository _repoCar;
        public ProdutoService(IProdutoRepository repo, ICarrinhoRepository repoCar)
        {
            _repo = repo;
            _repoCar = repoCar;
        }
        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Produto>> FindAll()
        {
            List<Produto> produtos = await _repo.FindAll();
            return produtos;
        }

        public async Task<Produto> FindById(int id)
        {
            Produto produto = await _repo.FindById(id);
            return produto;
        }

        public async Task<Produto> Save(Produto produto)
        {
            await _repo.Save(produto);
            return produto;
        }

        public async Task Update(Produto produto)
        {
            await _repo.Update(produto);
        }

        public async Task<List<ItemCarrinho>> ListarProdutosCarrinho(string email)
        {
            List<ItemCarrinho> carrinho = await _repoCar.Carrinho(email);
            return carrinho;
        }

        public async Task AddCarrinho(string email, int id)
        {
            await _repoCar.addCarrinho(email, id);
        }

        public async Task RemoverCarrinho(string email, int id)
        {
            await _repoCar.removeCarrinho(email, id);
        }

        public async Task<List<ProdutoMaisVendidoDTO>> ProdutosMaisVendidos()
        {
            return await _repo.ProdutosMaisVendidos();
        }

        public async Task<ImagemModel> buscarImagem(int id)
        {
            return await _repo.buscarImagem(id);
        }

        public async Task limparCarrinho(string email)
        {
            await _repoCar.limparCarrinho(email);
        }

    }
}
