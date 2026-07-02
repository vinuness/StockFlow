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
        public int EnderecoId { get; set; }
        public List<Endereco> Enderecos { get; set; } = new();
        public string Email { get; set; }
        public string Senha { get; set; }

        public Role Role { get; set; } = Role.CLIENTE;

        [JsonIgnore]
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
