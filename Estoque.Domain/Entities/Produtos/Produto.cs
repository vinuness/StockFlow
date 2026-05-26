namespace Estoque.Domain.Entities.Produtos
{
    public class Produto
    {
        public int Id { get; set; }
        public long SKU { get; set; }
        public string ImagemUrl { get; set; } = "";
        public string Nome { get; set; } = "";
        public int Quantidade { get; set; }
        public string Descricao { get; set; } = "";
        public decimal Preco { get; set; }
        public string Tamanho { get; set; } = "";
        public string Cor { get; set; } = "";

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public StatusProduto Status { get; set; }
    }
}