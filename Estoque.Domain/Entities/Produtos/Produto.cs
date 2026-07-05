using Estoque.Domain.Entities.Pedidos;
using System.Text.Json.Serialization;

namespace Estoque.Domain.Entities.Produtos
{
    public class Produto
    {
        public int Id { get; set; }
        public long SKU { get; set; }

        [JsonIgnore]
        public ImagemModel Imagens { get; set; } = new();
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

        [JsonIgnore]
        public List<ItemPedido> ItensPedido { get; set; } = new();
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

        [JsonIgnore]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

    }

}