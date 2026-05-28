using Estoque.Models;

namespace Estoque.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<List<PedidoModel>?> FindAll();
        Task<HttpResponseMessage> FazerPedido(List<EstoqueModel> produtos);
    }
}