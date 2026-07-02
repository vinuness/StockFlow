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
        private readonly HttpClient _http;

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
            LoginResponse usuario = await _clienteService.Login(login);

            if (usuario == null)
            {
                return Content("usuario nulo");
            }
            
            Response.Cookies.Append("jwt", usuario.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict, 
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });

            Response.Cookies.Append("email", usuario.Email);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {

            //remove os cookies armazenados
            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("email");

            return RedirectToAction("Logar", "Cliente");
        }

        public async Task<IActionResult> Perfil()
        {   
            // Recupera o email do cookie
            var email = Request.Cookies["email"];
            var jwt = Request.Cookies["jwt"];

            if(jwt == null || email == null) 
            {
                return RedirectToAction("Logar", "Cliente");
            }

            var cliente = await _clienteService.FindByEmail(email);

            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> AddEndereco(EnderecoModel endereco)
        {
            var email = Request.Cookies["email"];
            await _clienteService.AddEndereco(email, endereco);
            return RedirectToAction("Perfil");
        }

        public IActionResult AddEndereco()
        {
            return View();
        }
    }
}