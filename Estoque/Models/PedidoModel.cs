using Estoque.Models;
using System.Text.Json.Serialization;

namespace Estoque.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public StatusPedido Status { get; set; }

        [JsonPropertyName("itens")]
        public List<ItemPedidoModel> ItensPedido { get; set; } = new();
    }
}