using Microsoft.AspNetCore.Mvc;
using Estoque.Models;

namespace Estoque.Interfaces
{
    public interface IHomeService
    {
        public Task<List<ProdutoMaisVendidoDTO>> MaisVendidos();
    }
}
