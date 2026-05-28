using Estoque.Models;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.FindAll();
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaModel categoria)
        {
            await _categoriaService.Create(categoria);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.FindById(id);
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoriaModel categoria)
        {
            await _categoriaService.Update(categoria);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}