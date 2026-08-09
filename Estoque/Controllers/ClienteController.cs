using Estoque.Models.Cliente;
using Estoque.Services.Interfaces;
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
            _http = new HttpClient();
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
                TempData["Error"] = "Dados incorretos ou usuario inexistente.";
                return RedirectToAction("Logar");
            }
            
            Response.Cookies.Append("jwt", usuario.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict, 
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });

            return RedirectToAction("Index", "Estoque");
        }

        public async Task<IActionResult> Logout()
        {

            //remove os cookies armazenados
            Response.Cookies.Delete("jwt");

            return RedirectToAction("Index", "Estoque");
        }

        public async Task<IActionResult> Perfil()
        {   
            // Recupera o email do cookie
            var email = User.FindFirstValue(ClaimTypes.Email);
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
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _clienteService.AddAdress(email, endereco);
            return RedirectToAction("Perfil");
        }

        [HttpPost]
        public async Task<IActionResult> SetPrincipalAdress(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _clienteService.SetPrincipalAdress(email, id);
            return RedirectToAction("Perfil");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdress(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _clienteService.RemoveAdress(email, id);
            return RedirectToAction("Perfil");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAdress(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var endereco = await _clienteService.FindById(id, email);
            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdress(EnderecoModel dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _clienteService.UpdateAdress(email, dto.Id, dto);
            return RedirectToAction("Perfil");
        }

        public IActionResult AddEndereco()
        {
            return View();
        }
    }
}