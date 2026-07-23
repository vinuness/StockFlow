using Estoque.Domain.Entities.Pedidos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Estoque.Domain.Entities.Clientes
{
    public class Cliente
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public int EnderecoId { get; set; }
        public string Roles { get; set; }
        public List<Endereco> Enderecos { get; set; } = new();
        public string Email { get; set; }
        public string Senha { get; set; }

        [JsonIgnore]
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

        [JsonIgnore]
        public Carrinho Carrinho { get; set; }
    }
}
