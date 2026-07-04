using Estoque.Models;

namespace Estoque.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<List<PedidoModel>> FindAll(string email);
        Task<HttpResponseMessage> FazerPedido(List<ProdutoPedidoDTO> produtos, int id);
    }
}