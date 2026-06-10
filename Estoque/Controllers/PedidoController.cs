using Estoque.Models;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoService.FindAll();

            return View(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> FazerPedido(List<ProdutoPedidoDTO> produtos)
        {
            var pedido = await _pedidoService.FazerPedido(produtos);

            if (!pedido.IsSuccessStatusCode)
            {
                var erro = await pedido.Content.ReadAsStringAsync();

                return Content($"Erro ao salvar pedido:\n{erro}");
            }

            return RedirectToAction("Index");
        }
    }
}