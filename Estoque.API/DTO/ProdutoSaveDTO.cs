using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;
using System.Text.Json.Serialization;

namespace Estoque.API.DTO
{
    public class ProdutoSaveDTO
    {
        public IFormFile? Imagem { get; set; }
        public ImagemModel Imagens { get; set; } = new();
        public string Nome { get; set; } = "";
        public int Quantidade { get; set; }

        public string Descricao { get; set; } = "";
        public decimal Preco { get; set; }
        public string Tamanho { get; set; } = "";
        public string Cor { get; set; } = "";

        public int CategoriaId { get; set; }

        public int FornecedorId { get; set; }
    }
}
