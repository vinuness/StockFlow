using Estoque.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly HttpClient _http;
        public CategoriaController(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient();
        }
        public async Task<IActionResult> Index()
        {
            List<CategoriaModel> categorias = await _http.GetFromJsonAsync<List<CategoriaModel>>("https://localhost:7238/api/Categoria/findAll");
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
            await _http.PostAsJsonAsync("https://localhost:7238/api/Categoria/save", categoria);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _http.GetFromJsonAsync<CategoriaModel>(
                $"https://localhost:7238/api/Categoria/findById/{id}");

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoriaModel categoria)
        {

            var response = await _http.PutAsJsonAsync(
                $"https://localhost:7238/api/Categoria/update/{categoria.Id}",
                categoria);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content(error);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _http.DeleteAsync($"https://localhost:7238/api/Categoria/delete/{id}");
            return RedirectToAction("Index");
        }
    }
}
