using Estoque.Models.Cliente;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Estoque.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ClienteModel cliente)
        {
            await _clienteService.Create(cliente);

            return RedirectToAction("Logar");
        }

        public IActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logar(LoginModel login)
        {
            //pega o usuário que a API devolve depois do login
            var usuario = await _clienteService.Login(login);

            if (usuario is null)
            {
                ModelState.AddModelError("", "Email ou senha inválidos");
                return View(login);
            }

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
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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

            var cliente = await _clienteService.FindByEmail(email);

            return View(cliente);
        }
    }
}