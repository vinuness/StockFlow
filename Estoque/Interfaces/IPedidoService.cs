using Estoque.Models;

namespace Estoque.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<List<PedidoModel>> FindAll(string email);
        Task<HttpResponseMessage> FazerPedido(List<ProdutoPedidoDTO> produtos, string email);
        Task<double> Faturamento();
    }
}