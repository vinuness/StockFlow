using Estoque.Models;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        public async Task<IActionResult> Index()
        {
            var fornecedores = await _fornecedorService.FindAll();

            return View(fornecedores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FornecedorModel fornecedor)
        {
            await _fornecedorService.Create(fornecedor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var fornecedor = await _fornecedorService.FindById(id);

            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FornecedorModel fornecedor)
        {
            await _fornecedorService.Update(fornecedor);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _fornecedorService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}