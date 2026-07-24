using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IServices;

namespace Estoque.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _con;
        private readonly IEmailSender _email;
        public PedidoRepository(AppDbContext con, IEmailSender email)
        {
            _con = con;
            _email = email;
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

        public async Task<Pedido> Save(List<ProdutoPedidoDTO> produtos, string email)
        {
            var cliente = await _con.Clientes
                .Include(c => c.Carrinho)
                    .ThenInclude(c => c.Items)
                        .ThenInclude(i => i.Produto)
                .Include(c => c.Pedidos)
                .FirstOrDefaultAsync(c => c.Email == email);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            if (cliente.Carrinho == null)
                throw new Exception("Carrinho não encontrado.");

            // Atualiza as quantidades escolhidas pelo usuário
            foreach (var dto in produtos)
            {
                var itemCarrinho = cliente.Carrinho.Items
                    .FirstOrDefault(i => i.ProdutoId == dto.ProdutoId);

                if (itemCarrinho == null)
                    throw new Exception($"Produto {dto.ProdutoId} não encontrado no carrinho.");

                itemCarrinho.Quantidade = dto.Quantidade;
            }

            var pedido = new Pedido
            {
                DataPedido = DateTime.UtcNow,
                Status = StatusPedido.PENDENTE,
                Itens = new List<ItemPedido>()
            };

            foreach (var itemCarrinho in cliente.Carrinho.Items)
            {
                var produto = itemCarrinho.Produto;

                if (produto == null)
                    throw new Exception("Produto não encontrado.");

                if (produto.Quantidade < itemCarrinho.Quantidade)
                    throw new Exception($"Estoque insuficiente para {produto.Nome}");

                produto.Quantidade -= itemCarrinho.Quantidade;

                pedido.Itens.Add(new ItemPedido
                {
                    ProdutoId = produto.Id,
                    Quantidade = itemCarrinho.Quantidade,
                    PrecoUnitario = produto.Preco
                });
            }

            cliente.Pedidos.Add(pedido);

            _con.Pedidos.Add(pedido);

            // Esvazia o carrinho após finalizar a compra
            _con.ItensCarrinho.RemoveRange(cliente.Carrinho.Items);

            await _con.SaveChangesAsync();

            pedido = await _con.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                        .ThenInclude(p => p.Categoria)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                        .ThenInclude(p => p.Fornecedor)
                .FirstAsync(p => p.Id == pedido.Id);

            await _email.EmailPedido(cliente, pedido);

            return pedido;
        }

        public async Task Update(Pedido pedido, int id)
        {
            pedido.Id = id;
            _con.Pedidos.Update(pedido);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Pedido>> buscarPedidosDeCliente(string email)
        {
            Cliente cliente = await _con.Clientes
                .Include(c => c.Pedidos)
                .ThenInclude(p => p.Itens)
                .ThenInclude(item => item.Produto)
                .ThenInclude(pr => pr.Categoria)

                .Include(c => c.Pedidos)
                .ThenInclude(p => p.Itens)
                .ThenInclude(item => item.Produto)
                .ThenInclude(pr => pr.Fornecedor)
                .FirstOrDefaultAsync(c => c.Email == email);

            List<Pedido> pedidos = cliente.Pedidos.ToList();

            return pedidos;
        }

        public async Task<double> Faturamento()
        {
            return await _con.Pedidos
                .SelectMany(p => p.Itens)
                .SumAsync(i => (double)(i.PrecoUnitario * i.Quantidade));
        }
    }
}
