using Estoque.Models.Cliente;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using System.Security.Claims;

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

            if (!res.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Email ou senha inválidos");
                return View(login);
            }

            //pega o usuário que a API devolve depois do login
            var usuario = await res.Content.ReadFromJsonAsync<LoginResponse>(); 

            var claims = new List<Claim> //Lista as informações do usuario logado
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
            };

            //cria uma identidade a partir das informações do usuario autenticada via cookie
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //usuario final
            var principal = new ClaimsPrincipal(identity);

            //gera um cookie que será gravado no navegador, onde irá identificar o usuario como logado
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            //remove o cookie gerado pelo SignIn
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Logar", "Cliente");
        }

        [Authorize]
        public async Task<IActionResult> Perfil()
        {

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var cliente = await _http.GetFromJsonAsync<ClienteModel>($"https://localhost:7238/api/Cliente/findByEmail/{email}");

            return View(cliente);
        }
    }
}
