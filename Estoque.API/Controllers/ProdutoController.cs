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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _service;
        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet("findAll")]
        [Authorize(Roles = "Admin,Operador,Cliente")]
        public async Task<ActionResult<List<Produto>>> FindAll()
        {
            List<Produto> produtos = await _service.FindAll();
            return Ok(produtos);
        }

        [HttpGet("findById/{id}")]
        [Authorize(Roles = "Admin,Operador,Cliente")]
        public async Task<ActionResult<Produto>> FindById(int id)
        {
            Produto produto = await _service.FindById(id);
            if (produto == null)
            {
                return NotFound("Produto não existente");
            }
            return Ok(produto);
        }

        [HttpPost("save")]
        [Authorize(Roles = "Admin,Operador")]
        public async Task<ActionResult<Produto>> Save([FromBody] Produto produto)
        {
            await _service.Save(produto);
            return Ok(produto);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin,Operador")]
        public async Task<ActionResult> Update([FromBody] Produto produto, int id)
        {
            await _service.Update(produto, id);
            return Ok("Produto atualizado com sucesso");
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin,Operador")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Produto deletado com sucesso");
        }

        [HttpGet("carrinho")]
        public async Task<ActionResult<List<Produto>>> ListarProdutosCarrinho()
        {
            List<Produto> produtos = await _service.ListarProdutosCarrinho();
            return Ok(produtos);
        }

        [HttpPut("carrinho/add/{id}")]
        public async Task<ActionResult> AddCarrinho(int id)
        {
            await _service.AddCarrinho(id);
            return Ok("Produto adicionado ao carrinho com sucesso");
        }

        [HttpPut("carrinho/remove/{id}")]
        public async Task<ActionResult> RemoverCarrinho(int id)
        {
            await _service.RemoverCarrinho(id);
            return Ok("Produto removido do carrinho com sucesso");
        }

        [HttpGet("mais_vendidos")]
        public async Task<ActionResult<List<ProdutoMaisVendidoDTO>>> MaisVendidos()
        {
            var produtos = await _service.ProdutosMaisVendidos();
            return Ok(produtos);
        }
    }
}
