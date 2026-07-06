using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Produtos;
using System.Text.Json.Serialization;

namespace Estoque.Domain.Entities.Pedidos
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public StatusPedido Status { get; set; }
        public List<ItemPedido> Itens { get; set; } = new();
    }

    public class Faturamento
    {
        public double Valor { get; set; }
    }
}
