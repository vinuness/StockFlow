using Estoque.Interfaces;
using Estoque.Models;

namespace Estoque.Services
{
    public class HomeService : IHomeService
    {
        private readonly HttpClient _httpClient;
        private string produtos = "https://localhost:7238/api/Produto";

        public HomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProdutoMaisVendidoDTO>> MaisVendidos()
        {
            return await _httpClient.GetFromJsonAsync<List<ProdutoMaisVendidoDTO>>($"{produtos}/mais_vendidos");
        }
    }
}
