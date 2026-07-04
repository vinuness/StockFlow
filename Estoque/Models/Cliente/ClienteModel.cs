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

        public string Roles { get; set; }

        [Required]
        public List<EnderecoModel> Enderecos { get; set; } = new();

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
        public List<PedidoModel>? Pedidos { get; set; }
    }
}
