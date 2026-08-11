using Estoque.Application.Service;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _service;
        public FornecedorController(IFornecedorService service)
        {
            _service = service;
        }

        [HttpGet("findAll")]
        public async Task<ActionResult<List<Fornecedor>>> FindAll()
        {
            List<Fornecedor> fornecedores = await _service.FindAll();
            return Ok(fornecedores);
        }

        [HttpGet("findById/{id}")]
        public async Task<ActionResult<Fornecedor>> FindById(int id)
        {
            Fornecedor fornecedor = await _service.FindById(id);
            if (fornecedor == null)
            {
                return NotFound("Fornecedor não existente");
            }
            return Ok(fornecedor);
        }

        [HttpPost("save")]
        [Authorize(Roles = "Admin, Operador")]
        public async Task<ActionResult<Fornecedor>> Save([FromBody] Fornecedor fornecedor)
        {
            await _service.Save(fornecedor);
            return Ok(fornecedor);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin, Operador")]
        public async Task<ActionResult> Update([FromBody] Fornecedor fornecedor, int id)
        {
            await _service.Update(fornecedor, id);
            return Ok("Fornecedor atualizado com sucesso");
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin, Operador")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Fornecedor deletado com sucesso");
        }
    }
}
