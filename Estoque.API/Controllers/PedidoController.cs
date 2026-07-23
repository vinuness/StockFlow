using Estoque.Application.Service;
using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Pedido>>> FindAll()
        {
            List<Pedido> pedidos = await _service.FindAll();
            return Ok(pedidos);
        }

        [HttpGet("findById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Pedido>> FindById(int id)
        {
            Pedido pedido = await _service.FindById(id);
            if (pedido == null)
            {
                return NotFound("Pedido não existente");
            }
            return Ok(pedido);
        }

        [HttpPost("save/pedido/user/{email}")]
        public async Task<ActionResult<Pedido>> Save([FromBody] List<ProdutoPedidoDTO> produtos, string email)
        {
            await _service.Save(produtos, email);
            return Ok("Produto salvo com sucesso");
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] Pedido pedido, int id)
        {
            await _service.Update(pedido, id);
            return Ok("Pedido atualizado com sucesso");
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Pedido deletado com sucesso");
        }

        [HttpGet("buscarPedidosDeCliente/{email}")]
        public async Task<ActionResult<List<Pedido>>> buscarPedidosDeCliente(string email)
        {
            List<Pedido> pedidos = await _service.buscarPedidosDeCliente(email);
            return Ok(pedidos);
        }

        [HttpGet("faturamento")]
        public async Task<ActionResult<double>> Faturamento() {
            double valor = await _service.Faturamento();
            return Ok(valor);
        }
    }
}
