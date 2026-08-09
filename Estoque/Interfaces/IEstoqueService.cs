using Estoque.Models;
using Estoque.Models.Carrinho;
using Estoque.Pagination;

namespace Estoque.Services.Interfaces
{
    public interface IEstoqueService
    {
        Task<List<EstoqueModel>?> FindAll(PaginationParams Pageparams);
        Task<EstoqueModel?> FindById(int id);
        Task Create(ProdutoCreateViewModel produto);
        Task Update(ProdutoCreateViewModel produto, int id);
        Task Delete(int id);
        Task<List<ItemCarrinho>?> Carrinho(string email);
        Task AddCarrinho(string email, int id);
        Task RemoverCarrinho(string email, int id);
        Task limparCarrinho(string email);
    }
}