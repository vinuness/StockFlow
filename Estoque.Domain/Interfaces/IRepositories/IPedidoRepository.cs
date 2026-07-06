using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Interfaces.IRepositories
{
    public interface IPedidoRepository
    {
        public Task<List<Pedido>> FindAll();
        public Task<Pedido> FindById(int id);
        public Task<Pedido> Save(List<ProdutoPedidoDTO> produtos, int id);
        public Task Update(Pedido pedido, int id);
        public Task Delete(int id);
        public Task<List<Pedido>> buscarPedidosDeCliente(string email);
        public Task<double> Faturamento();
    }
}
