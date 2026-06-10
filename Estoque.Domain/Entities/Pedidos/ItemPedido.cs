using Estoque.Domain.Entities.Produtos;
using System.Text.Json.Serialization;

namespace Estoque.Domain.Entities.Pedidos
{
    public class ItemPedido
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        [JsonIgnore]
        public Pedido Pedido { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }
    }
}