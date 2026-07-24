using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Pedidos;

namespace Estoque.Domain.Interfaces.IServices
{
    public interface IEmailSender
    {
        public Task SendEmail(Cliente cliente);
        public Task EmailPedido(Cliente cliente, Pedido pedido);
        public Task EmailCarrinho(Cliente cliente);
    }
}
