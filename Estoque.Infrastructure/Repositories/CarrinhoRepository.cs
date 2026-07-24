using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;
using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infrastructure.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {

        private readonly AppDbContext _con;
        private readonly IEmailSender _email;
        public CarrinhoRepository(AppDbContext con, IEmailSender email)
        {
            _con = con;
            _email = email;
        }

        public async Task<List<ItemCarrinho>> Carrinho(string email)
        {
            return await _con.ItensCarrinho
                .Include(i => i.Produto)
                    .ThenInclude(p => p.Categoria)
                .Include(i => i.Produto)
                    .ThenInclude(p => p.Fornecedor)
                .Where(i => i.Carrinho.Cliente.Email == email)
                .ToListAsync();
        }

        public async Task addCarrinho(string email, int produtoId)
        {
            var cliente = await _con.Clientes
                .Include(c => c.Carrinho)
                .ThenInclude(ca => ca.Items)
                .FirstOrDefaultAsync(c => c.Email == email);

            if (cliente == null)
                throw new Exception($"cliente não encontrado: {cliente.Email}");

            if (cliente.Carrinho == null)
                cliente.Carrinho = new Carrinho
                {
                    Items = new List<ItemCarrinho>()
                };

            if (cliente.Carrinho.Items == null)
                cliente.Carrinho.Items = new List<ItemCarrinho>();

            var item = cliente.Carrinho.Items.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item != null)
            {
                item.Quantidade++;
            }

            cliente.Carrinho.Items.Add(new ItemCarrinho
            {
                ProdutoId = produtoId,
                Quantidade = 1
            });

            await _con.SaveChangesAsync();
        }

        public async Task removeCarrinho(string email, int produtoId)
        {
            var item = await _con.ItensCarrinho
                .Include(i => i.Carrinho)
                .ThenInclude(c => c.Cliente)
                .FirstOrDefaultAsync(i =>
                    i.ProdutoId == produtoId &&
                    i.Carrinho.Cliente.Email == email);

            if (item == null) return;

            _con.ItensCarrinho.Remove(item);

            await _con.SaveChangesAsync();
        }

        public async Task limparCarrinho(string email)
        {

            var itens = await _con.ItensCarrinho
                .Include(i => i.Carrinho)
                .ThenInclude(c => c.Cliente)
                .Where(i => i.Carrinho.Cliente.Email == email)
                .ToListAsync();

            _con.ItensCarrinho.RemoveRange(itens);

            await _con.SaveChangesAsync();
        }

        public async Task VerificarCarrinhos()
        {
            var clientes = await _con.Clientes
                .Include(c => c.Carrinho)
                    .ThenInclude(c => c.Items)
                        .ThenInclude(i => i.Produto)
                            .ThenInclude(p => p.Categoria)

                .Include(c => c.Carrinho)
                    .ThenInclude(c => c.Items)
                        .ThenInclude(i => i.Produto)
                            .ThenInclude(p => p.Fornecedor)
                .Where(c => c.Carrinho != null && c.Carrinho.Items.Any())
                .ToListAsync();

            foreach (var cliente in clientes)
            {
                await _email.EmailCarrinho(cliente);
            }
        }
    }
}
