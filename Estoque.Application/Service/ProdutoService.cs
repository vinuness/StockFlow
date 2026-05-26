using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;

namespace Estoque.Application.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repo;
        public ProdutoService(IProdutoRepository repo)
        {
            _repo = repo;
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

        public async Task Update(Produto produto, int id)
        {
            await _repo.Update(produto, id);
        }

        public async Task<List<Produto>> ListarProdutosCarrinho()
        {
            List<Produto> produtos = await _repo.FindAll();
            List<Produto> carrinho = new List<Produto>();
            foreach (var produto in produtos)
            {
                if (produto.Status.Equals(StatusProduto.CARRINHO))
                {
                    carrinho.Add(produto);
                }
            }
            return carrinho;
        }

        public async Task AddCarrinho(int id)
        {
            Produto produto = await _repo.FindById(id);
            produto.Status = StatusProduto.CARRINHO;
            await _repo.Update(produto, id);
        }

        public async Task RemoverCarrinho(int id)
        {
            Produto produto = await _repo.FindById(id);
            produto.Status = StatusProduto.CATALOGADO;
            await _repo.Update(produto, id);
        }
    }
}
