using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;

namespace Estoque.Models
{
    public class EstoqueModel
    {
        public int Id { get; set; }
        public long SKU { get; set; }
        public ImagemModel Imagem { get; set; }
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
        public List<ItemPedidoModel> ItensPedido { get; set; } = new();
    }

    public class ProdutoPedidoDTO
    {
        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }
    }

    public class ProdutoMaisVendidoDTO
    {
        public string Nome { get; set; } = "";

        public int QuantidadeVendida { get; set; }
    }
    public class ImagemModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public int ProdutoId { get; set; }

    }
    public class ProdutoCreateViewModel
    {
        public string Nome { get; set; }

        public int Quantidade { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public string Tamanho { get; set; }

        public string Cor { get; set; }

        public int CategoriaId { get; set; }

        public int FornecedorId { get; set; }

        public IFormFile? Imagem { get; set; }
    }
}
