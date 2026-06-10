namespace Estoque.Models
{
    public class ItemPedidoModel
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public int ProdutoId { get; set; }

        public EstoqueModel Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }
    }
}