using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace Estoque.Models.Cliente
{
    public class ClienteModel
    {
        public int Id { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public List<EnderecoModel> Enderecos { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public Role Role { get; set; } = Role.CLIENTE;
        public List<PedidoModel>? Pedidos { get; set; }
    }
}
