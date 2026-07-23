namespace Estoque.Models.Carrinho
{
    public class ItemCarrinho
    {
        public int Id { get; set; }

        public int CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }

        public int ProdutoId { get; set; }
        public EstoqueModel Produto { get; set; }

        public int Quantidade { get; set; }
    }
}
