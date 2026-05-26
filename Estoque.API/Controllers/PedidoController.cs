using Estoque.Application.Service;
using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;
        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet("findAll")]
        public async Task<ActionResult<List<Pedido>>> FindAll()
        {
            List<Pedido> pedidos = await _service.FindAll();
            return Ok(pedidos);
        }

        [HttpGet("findById/{id}")]
        public async Task<ActionResult<Pedido>> FindById(int id)
        {
            Pedido pedido = await _service.FindById(id);
            if (pedido == null)
            {
                return NotFound("Pedido não existente");
            }
            return Ok(pedido);
        }

        [HttpPost("save")]
        public async Task<ActionResult<Pedido>> Save(List<Produto> produtos)
        {
            await _service.Save(produtos);
            return Ok("Produto salvo com sucesso");
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update([FromBody] Pedido pedido, int id)
        {
            await _service.Update(pedido, id);
            return Ok("Pedido atualizado com sucesso");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Pedido deletado com sucesso");
        }
    }
}
