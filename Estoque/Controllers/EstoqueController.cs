using Estoque.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Estoque.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly HttpClient _http;

        public EstoqueController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _http.GetFromJsonAsync<List<EstoqueModel>>("https://localhost:7238/api/Produto/findAll");

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
            produto.Categoria = await _http.GetFromJsonAsync<CategoriaModel>(
            $"https://localhost:7238/api/Categoria/findById/{produto.CategoriaId}");

            produto.Fornecedor = await _http.GetFromJsonAsync<FornecedorModel>(
            $"https://localhost:7238/api/Fornecedor/findById/{produto.FornecedorId}");
            await _http.PostAsJsonAsync("https://localhost:7238/api/Produto/save", produto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _http.GetFromJsonAsync<EstoqueModel>(
                $"https://localhost:7238/api/Produto/findById/{id}");

            produto.Categoria = await _http.GetFromJsonAsync<CategoriaModel>(
                $"https://localhost:7238/api/Categoria/findById/{produto.CategoriaId}");

            produto.Fornecedor = await _http.GetFromJsonAsync<FornecedorModel>(
                $"https://localhost:7238/api/Fornecedor/findById/{produto.FornecedorId}");

            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EstoqueModel produto)
        {

            produto.Categoria = await _http.GetFromJsonAsync<CategoriaModel>(
            $"https://localhost:7238/api/Categoria/findById/{produto.CategoriaId}");

            produto.Fornecedor = await _http.GetFromJsonAsync<FornecedorModel>(
            $"https://localhost:7238/api/Fornecedor/findById/{produto.FornecedorId}");

            await _http.PutAsJsonAsync(
                $"https://localhost:7238/api/Produto/update/{produto.Id}",produto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _http.DeleteAsync($"https://localhost:7238/api/Produto/delete/{id}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Carrinho()
        {
            var carrinho = await _http.GetFromJsonAsync<List<EstoqueModel>>("https://localhost:7238/api/Produto/carrinho");
            return View(carrinho);
        }

        public async Task<IActionResult> AddCarrinho(int id)
        {
            var produto = await _http.GetFromJsonAsync<EstoqueModel>(
                $"https://localhost:7238/api/Produto/findById/{id}");

            if (produto.Status == StatusProduto.CATALOGADO)
            {
                produto.Status = StatusProduto.CARRINHO;
                await _http.PutAsJsonAsync($"https://localhost:7238/api/Produto/carrinho/add/{id}", produto);
            }
            return RedirectToAction("Carrinho");
        }

        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            var produto = await _http.GetFromJsonAsync<EstoqueModel>(
                $"https://localhost:7238/api/Produto/findById/{id}");

            if(produto.Status == StatusProduto.CARRINHO)
            {
                produto.Status = StatusProduto.CATALOGADO;
                await _http.PutAsJsonAsync($"https://localhost:7238/api/Produto/carrinho/remove/{id}", produto);
            }
            return RedirectToAction("Carrinho");
        }
    }
}