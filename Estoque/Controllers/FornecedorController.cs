using System.Net.Http.Json;
using Estoque.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly HttpClient _http;
        public FornecedorController(IHttpClientFactory factory) { 
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            List<FornecedorModel> fornecedores = await _http.GetFromJsonAsync<List<FornecedorModel>>("https://localhost:7238/api/Fornecedor/findAll");
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
            await _http.PostAsJsonAsync("https://localhost:7238/api/Fornecedor/save", fornecedor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var fornecedor = await _http.GetFromJsonAsync<FornecedorModel>(
                $"https://localhost:7238/api/Fornecedor/findById/{id}");

            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FornecedorModel fornecedor)
        {
            await _http.PutAsJsonAsync(
                $"https://localhost:7238/api/Fornecedor/update/{fornecedor.Id}",
                fornecedor);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id) {
            await _http.DeleteAsync($"https://localhost:7238/api/Fornecedor/delete/{id}");
            return RedirectToAction("Index");
        }
    }
}
