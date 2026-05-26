using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Entities.Produtos;

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
            List<Produto> produtos = await _con.Produtos.ToListAsync();
            foreach (var item in produtos)
            {
                item.Categoria = await _con.Categorias.FindAsync(item.CategoriaId);
                item.Fornecedor = await _con.Fornecedores.FindAsync(item.FornecedorId);
            }
            return produtos;
        }

        public async Task<Produto> FindById(int id) 
        {
            Produto produto = await _con.Produtos.FindAsync(id);
            produto.Categoria = await _con.Categorias.FindAsync(produto.CategoriaId);
            produto.Fornecedor = await _con.Fornecedores.FindAsync(produto.FornecedorId);

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

        public async Task Update(Produto produto, int id)
        {
            produto.Id = id;
            produto.Categoria = await _con.Categorias.FindAsync(produto.CategoriaId);
            produto.Fornecedor = await _con.Fornecedores.FindAsync(produto.FornecedorId);
            _con.Produtos.Update(produto);
            await _con.SaveChangesAsync();
        }
    }
}
