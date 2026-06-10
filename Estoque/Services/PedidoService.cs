using Estoque.Models;
using Estoque.Services.Interfaces;

namespace Estoque.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://localhost:7238/api/Pedido";

        public PedidoService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<List<PedidoModel>?> FindAll()
        {
            return await _http.GetFromJsonAsync<List<PedidoModel>>($"{BaseUrl}/findAll");
        }

        public async Task<HttpResponseMessage> FazerPedido(List<ProdutoPedidoDTO> produtos)
        {
            return await _http.PostAsJsonAsync($"{BaseUrl}/save", produtos);
        }
    }
}