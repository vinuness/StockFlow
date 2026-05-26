using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Application.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repo;
        public PedidoService(IPedidoRepository repo)
        {
            _repo = repo;
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Pedido>> FindAll()
        {
            List<Pedido> pedidos = await _repo.FindAll();
            return pedidos;
        }

        public async Task<Pedido> FindById(int id)
        {
            Pedido pedido = await _repo.FindById(id);
            return pedido;
        }

        public async Task<Pedido> Save(List<Produto> produtos)
        {
            var pedido = await _repo.Save(produtos);
            return pedido;
        }

        public async Task Update(Pedido pedido, int id)
        {
            await _repo.Update(pedido, id);
        }
    }
}
