using Estoque.Domain.Entities.Pedidos;
using System.ComponentModel.DataAnnotations;

namespace Estoque.Domain.Entities.Clientes
{
    public class Cliente
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public Endereço Endereço { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }

        public Role Role { get; set; } = Role.CLIENTE;
        public List<Pedido> Pedidos { get; set; }
    }
}
