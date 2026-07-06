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
            _http = factory.CreateClient("API");
        }

        public async Task<List<PedidoModel>> FindAll(string email)
        {
            return await _http.GetFromJsonAsync<List<PedidoModel>>($"{BaseUrl}/buscarPedidosDeCliente/{email}");
        }

        public async Task<HttpResponseMessage> FazerPedido(List<ProdutoPedidoDTO> produtos, int id)
        {
            return await _http.PostAsJsonAsync($"{BaseUrl}/save/pedido/user/{id}", produtos);
        }

        public async Task<double> Faturamento()
        {
            return await _http.GetFromJsonAsync<double>("https://localhost:7238/api/Pedido/faturamento");
        }
    }
}