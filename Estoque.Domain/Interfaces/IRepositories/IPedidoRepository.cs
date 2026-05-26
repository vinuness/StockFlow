using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Interfaces.IRepositories
{
    public interface IPedidoRepository
    {
        public Task<List<Pedido>> FindAll();
        public Task<Pedido> FindById(int id);
        public Task<Pedido> Save(List<Produto> produtos);
        public Task Update(Pedido pedido, int id);
        public Task Delete(int id);
    }
}
