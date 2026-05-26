using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Estoque.Models.Cliente
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public EnderecoModel Endereço { get; set; } = new();
        public string Email { get; set; }
        public string Senha { get; set; }
        public Role Role { get; set; } = Role.CLIENTE;
        public List<PedidoModel> Pedidos { get; set; }
    }
}
