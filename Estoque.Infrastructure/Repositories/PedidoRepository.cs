using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;

namespace Estoque.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _con;
        public PedidoRepository(AppDbContext con)
        {
            _con = con;
        }

        public async Task Delete(int id)
        {
            Pedido pedido = await _con.Pedidos.FindAsync(id);
            _con.Pedidos.Remove(pedido);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Pedido>> FindAll()
        {
            List<Pedido> pedidos = await _con.Pedidos

                .Include(p => p.Itens)
                .ThenInclude(item => item.Produto)
                .ThenInclude(produto => produto.Categoria)

                .Include(p => p.Itens)
                .ThenInclude(item => item.Produto)
                .ThenInclude(produto => produto.Fornecedor).ToListAsync();

            return pedidos;
        }

        public async Task<Pedido> FindById(int id)
        {
            Pedido pedido = await _con.Pedidos

                .Include(p => p.Itens)
                .ThenInclude(item => item.Produto)
                .ThenInclude(produto => produto.Categoria)

                .Include(p => p.Itens)
                .ThenInclude(item => item.Produto)
                .ThenInclude(produto => produto.Fornecedor)

                .FirstOrDefaultAsync(p => p.Id == id);

            return pedido;
        }

        public async Task<Pedido> Save(List<ProdutoPedidoDTO> produtos)
        {
            var pedido = new Pedido
            {
                DataPedido = DateTime.Now,
                Status = StatusPedido.PENDENTE,
                Itens = new List<ItemPedido>()
            };

            foreach (var item in produtos)
            {
                var produtoBanco = await _con.Produtos.FindAsync(item.ProdutoId);

                if (produtoBanco == null) throw new Exception("Produto não encontrado");

                if (produtoBanco.Quantidade < item.Quantidade) throw new Exception($"Estoque insuficiente para {produtoBanco.Nome}");

                produtoBanco.Quantidade -= item.Quantidade;
                _con.Produtos.Update(produtoBanco);

                pedido.Itens.Add(new ItemPedido
                {
                    ProdutoId = produtoBanco.Id,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = produtoBanco.Preco
                });
            }

            _con.Pedidos.Add(pedido);

            await _con.SaveChangesAsync();

            return pedido;
        }

        public async Task Update(Pedido pedido, int id)
        {
            pedido.Id = id;
            _con.Pedidos.Update(pedido);
            await _con.SaveChangesAsync();
        }
    }
}
