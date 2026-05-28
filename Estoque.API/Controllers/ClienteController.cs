using Estoque.Application.Service;
using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet("findAll")]
        public async Task<ActionResult<List<Cliente>>> FindAll()
        {
            List<Cliente> clientes = await _service.FindAll();
            return Ok(clientes);
        }

        [HttpGet("findById/{id}")]
        public async Task<ActionResult<Cliente>> FindById(int id)
        {
            Cliente cliente = await _service.FindById(id);
            return Ok(cliente);
        }

        [HttpGet("findByEmail/{email}")]
        public async Task<ActionResult<Cliente>> FindByEmail(string email)
        {
            return await _service.FindByEmail(email);
        }

        [HttpPost("save")]
        public async Task<ActionResult<Cliente>> save([FromBody] Cliente cliente)
        {
            await _service.Save(cliente);
            return Ok(cliente);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update([FromBody] Cliente cliente, int id)
        {
            await _service.Update(cliente, id);
            return Ok("Usuario atualizado com sucesso");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Usuario deletado com sucesso");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            var cliente = await _service.Login(login.Email, login.Senha);

            if(cliente == null)
            {
                return Unauthorized();
            }

            var response = new LoginResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email
            };

            return Ok(response);
        }
    }
}
