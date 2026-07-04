using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Estoque.Application.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using Estoque.Domain.Interfaces.IServices;
using Estoque.Domain.Entities.Produtos;
using Microsoft.AspNetCore.Authorization;

namespace Estoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Operador")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;
        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet("findAll")]
        public async Task<ActionResult<List<Categoria>>> FindAll()
        {
            List<Categoria> categorias = await _service.FindAll();
            return Ok(categorias);
        }

        [HttpGet("findById/{id}")]
        public async Task<ActionResult<Categoria>> FindById(int id)
        {
            Categoria categoria = await _service.FindById(id);
            if (categoria == null)
            {
                return NotFound("Categoria não existente");
            }
            return Ok(categoria);
        }

        [HttpPost("save")]
        public async Task<ActionResult<Categoria>> Save([FromBody] Categoria categoria)
        {
            await _service.Save(categoria);
            return Ok(categoria);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update([FromBody] Categoria categoria, int id)
        {
            await _service.Update(categoria, id);
            return Ok("Categoria atualizada com sucesso");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Categoria deletada com sucesso");
        }
    }
}
