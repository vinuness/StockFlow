using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Entities.Clientes;

namespace Estoque.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _con;
        public ProdutoRepository(AppDbContext con)
        {
            _con = con;
        }

        private Random rand;

        private async Task<long> GerarSKU()
        {
            rand = new Random();
            long sku;

            do
            {
                sku = rand.NextInt64(100000000000, 999999999999);
            } while (await _con.Produtos.AnyAsync(p => p.SKU == sku));
            return sku;
        }

        public async Task Delete(int id)
        {
            Produto produto = await _con.Produtos.FindAsync(id);
            _con.Produtos.Remove(produto);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Produto>> FindAll()
        {
            List<Produto> produtos = await _con.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .Include(p => p.Imagens)
                .ToListAsync();

            return produtos;
        }

        public async Task<Produto> FindById(int id)
        {
            Produto produto = await _con.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .Include(p => p.Imagens)
                .FirstOrDefaultAsync(p => p.Id == id);

            return produto;
        }

        public async Task<Produto> Save(Produto produto)
        {
            produto.SKU = await GerarSKU();
            produto.Categoria = await _con.Categorias.FindAsync(produto.CategoriaId);
            produto.Fornecedor = await _con.Fornecedores.FindAsync(produto.FornecedorId);
            produto.Status = StatusProduto.CATALOGADO;
            _con.Produtos.Add(produto);
            await _con.SaveChangesAsync();
            return produto;
        }

        public async Task Update(Produto produtoAtualizado)
        {
            var produto = await _con.Produtos
                .Include(p => p.Imagens)
                .FirstOrDefaultAsync(p => p.Id == produtoAtualizado.Id);

            if (produto == null) throw new Exception("produto nao encontrado");

            produto.Nome = produtoAtualizado.Nome;
            produto.Descricao = produtoAtualizado.Descricao;
            produto.Preco = produtoAtualizado.Preco;
            produto.Quantidade = produtoAtualizado.Quantidade;
            produto.Cor = produtoAtualizado.Cor;
            produto.Tamanho = produtoAtualizado.Tamanho;
            produto.CategoriaId = produtoAtualizado.CategoriaId;
            produto.FornecedorId = produtoAtualizado.FornecedorId;

            await _con.SaveChangesAsync();
        }

        public async Task<List<ProdutoMaisVendidoDTO>> ProdutosMaisVendidos()
        {
            return await _con.Produtos
                .Select(p => new ProdutoMaisVendidoDTO
                {
                    Nome = p.Nome,
                    QuantidadeVendida = p.ItensPedido.Sum(i => (int?)i.Quantidade) ?? 0
                })
                .OrderByDescending(p => p.QuantidadeVendida)
                .Take(5)
                .ToListAsync();
        }

        public async Task<ImagemModel> buscarImagem(int id)
        {
            return await _con.Imagens.FindAsync(id);
        }
    }
}