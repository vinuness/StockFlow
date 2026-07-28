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

        [HttpGet("find/{email}/adress/{id}")]
        public async Task<ActionResult<Endereco>> FindById(string email, int id)
        {
            var endereco = await _service.FindById(email, id);

            if (endereco == null)
                return NotFound();

            return Ok(endereco);
        }

        [HttpPost("save/user/{email}")]
        public async Task<ActionResult> Save(string email, [FromBody] EnderecoDTO endereco)
        {
            await _service.Save(email, endereco);
            return Ok(endereco);
        }

        [HttpPut("update/{email}/adress/{id}")]
        public async Task<ActionResult> Update(string email, int id, [FromBody] Endereco endereco)
        {
            await _service.Update(email, id, endereco);
            return Ok("Endereco atualizado com sucesso");
        }

        [HttpDelete("delete/{email}/adress/{id}")]
        public async Task<ActionResult> Delete(string email, int id)
        {
            await _service.Delete(email, id);
            return Ok("Endereco deletado com sucesso");
        }

        [HttpPut("set/{email}/principal/adress/{id}")]
        public async Task<ActionResult> SetPrincipalAdress(string email, int id)
        {
            await _service.SetPrincipalAdress(email, id);
            return Ok("Enderenco principal alterado com sucesso");
        }
    }
}