using Estoque.Models;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IEstoqueService _estoqueService;

        public EstoqueController(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _estoqueService.FindAll();

            return View(produtos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EstoqueModel produto)
        {
            await _estoqueService.Create(produto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _estoqueService.FindById(id);

            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EstoqueModel produto)
        {
            await _estoqueService.Update(produto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _estoqueService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Carrinho()
        {
            var carrinho = await _estoqueService.Carrinho();

            return View(carrinho);
        }

        public async Task<IActionResult> AddCarrinho(int id)
        {
            await _estoqueService.AddCarrinho(id);

            return RedirectToAction("Carrinho");
        }

        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            await _estoqueService.RemoverCarrinho(id);

            return RedirectToAction("Carrinho");
        }
    }
}