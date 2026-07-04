using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IClienteService _service;

        public UsuarioController(IClienteService service)
        {
            _service = service;
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult<Cliente>> save([FromBody] Cliente cliente)
        {
            await _service.Save(cliente);
            return Ok(cliente);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            var response = await _service.Login(login);

            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }
    }
}
