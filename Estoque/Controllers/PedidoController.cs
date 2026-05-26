using Estoque.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class PedidoController : Controller
    {
        private readonly HttpClient _http;

        public PedidoController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            List<PedidoModel> pedidos =
                await _http.GetFromJsonAsync<List<PedidoModel>>
                ("https://localhost:7238/api/Pedido/findAll");

            return View(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> FazerPedido(List<EstoqueModel> produtos)
        {
            var pedido = await _http.PostAsJsonAsync("https://localhost:7238/api/Pedido/save", produtos);

            if (!pedido.IsSuccessStatusCode)
            {
                var erro = await pedido.Content.ReadAsStringAsync();

                return Content($"Erro ao salvar pedido:\n{erro}");
            }

            return RedirectToAction("Index");
        }
    }
}