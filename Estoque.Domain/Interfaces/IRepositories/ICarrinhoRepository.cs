using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Produtos;


namespace Estoque.Domain.Interfaces.IRepositories
{
    public interface ICarrinhoRepository
    {
        public Task<List<ItemCarrinho>> Carrinho(string email);
        public Task addCarrinho(string email, int id);
        public Task removeCarrinho(string email, int id);
        public Task limparCarrinho(string email);

    }
}
