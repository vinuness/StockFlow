namespace Estoque.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public StatusPedido Status { get; set; }
        public List<EstoqueModel>? Produtos { get; set; }
    }
}
