using Microsoft.AspNetCore.Mvc.Rendering;

namespace Estoque.Models
{
    public class EstoqueModel
    {
        public int Id { get; set; }
        public long SKU { get; set; }
        public string ImagemUrl { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string Tamanho { get; set; }
        public string Cor { get; set; }

        public int CategoriaId { get; set; }
        public CategoriaModel Categoria { get; set; }

        public int FornecedorId { get; set; }
        public FornecedorModel Fornecedor { get; set; }
        public StatusProduto Status { get; set; }
    }
}
