using Estoque.Interfaces;
using Estoque.Models;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Estoque.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _service;
        private readonly IPedidoService _pedidoService;

        public HomeController(ILogger<HomeController> logger, IHomeService service, IPedidoService pedidoService)
        {
            _logger = logger;
            _service = service;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var jwt = Request.Cookies["jwt"];

            if (jwt == null)
            {
                return RedirectToAction("Logar", "Cliente");
            }

            var faturamento = await _pedidoService.Faturamento();
            ViewBag.Faturamento = faturamento;

            List<ProdutoMaisVendidoDTO> maisVendidos = await _service.MaisVendidos();
            return View(maisVendidos);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFaturamento()
        {
            var faturamento = await _pedidoService.Faturamento();

            var conteudo = $"Faturamento Total: {faturamento:C}";

            var bytes = Encoding.UTF8.GetBytes(conteudo);

            return File(bytes, "text/plain", "faturamento.txt");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
