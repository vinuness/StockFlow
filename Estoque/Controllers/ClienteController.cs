using Estoque.Models.Cliente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;

namespace Estoque.Controllers
{
    public class ClienteController : Controller
    {
        private readonly HttpClient _http;

        public ClienteController(IHttpClientFactory options)
        {
            _http = options.CreateClient();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ClienteModel cliente)
        {
            await _http.PostAsJsonAsync("https://localhost:7238/api/Cliente/save", cliente);
            return RedirectToAction("Logar");
        }

        public IActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logar(LoginModel login)
        {
            var res = await _http.PostAsJsonAsync("https://localhost:7238/api/Cliente/login", login);
            

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", $"Email ou senha inválidos");
            return View(login);
        }
    }
}
