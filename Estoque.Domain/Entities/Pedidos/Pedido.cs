using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Entities.Pedidos
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public StatusPedido Status { get; set; }
        public List<Produto>? Produtos { get; set; }
    }
}
