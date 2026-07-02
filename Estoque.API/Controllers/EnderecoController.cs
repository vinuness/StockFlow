using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _service;

        public EnderecoController(IEnderecoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Endereco>>> FindAll()
        {
            return Ok(await _service.FindAll());
        }

        [HttpGet("findById/{id}")]
        public async Task<ActionResult<Endereco>> FindById(int id)
        {
            var endereco = await _service.FindById(id);

            if (endereco == null)
                return NotFound();

            return Ok(endereco);
        }

        [HttpPost("save/user/{email}")]
        public async Task<IActionResult> Save(string email, [FromBody] EnderecoDTO endereco)
        {
            await _service.Save(email, endereco);
            return Ok(endereco);
        }

        [HttpPut("update{id}")]
        public async Task<IActionResult> Update([FromBody] Endereco endereco)
        {
            await _service.Update(endereco);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}