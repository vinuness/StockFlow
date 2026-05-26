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
                
                .Include(p => p.Produtos)
                .ThenInclude(produto => produto.Categoria)

                .Include(p => p.Produtos)
                .ThenInclude(produto => produto.Fornecedor).ToListAsync();
            return pedidos;
        }

        public async Task<Pedido> FindById(int id)
        {
            Pedido pedido = await _con.Pedidos.FindAsync(id);
            return pedido;
        }

        public async Task<Pedido> Save(List<Produto> produtos)
        {
            var produtoIds = produtos.Select(p => p.Id).ToList();

            var produtosBanco = await _con.Produtos
                .Where(p => produtoIds.Contains(p.Id))
                .ToListAsync();

            var pedido = new Pedido
            {
                DataPedido = DateTime.Now,
                Status = StatusPedido.PENDENTE,
                Produtos = produtosBanco
            };

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
