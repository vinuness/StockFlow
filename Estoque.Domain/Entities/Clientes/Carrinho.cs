using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Entities.Clientes
{
    public class Carrinho
    {
        public int Id { get; set; }
        public List<ItemCarrinho> Items { get; set; } = new();

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
