using Estoque.Models;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var email = User.FindFirstValue(ClaimTypes.Email);

            Console.WriteLine(email);

            var pedidos = await _pedidoService.FindAll(email);

            return View(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> FazerPedido(List<ProdutoPedidoDTO> produtos)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var pedido = await _pedidoService.FazerPedido(produtos, email);

            if (!pedido.IsSuccessStatusCode)
            {
                var erro = await pedido.Content.ReadAsStringAsync();

                return Content($"Erro ao salvar pedido:\n{erro}");
            }

            return RedirectToAction("Index");
        }
    }
}