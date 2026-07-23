using Estoque.Models.Cliente;

namespace Estoque.Models.Carrinho
{
    public class Carrinho
    {
        public int Id { get; set; }
        public List<ItemCarrinho> Items { get; set; } = new();

        public int ClienteId { get; set; }
        public ClienteModel Cliente { get; set; }
    }
}
