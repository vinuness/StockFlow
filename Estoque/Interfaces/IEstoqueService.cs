using Estoque.Models;

namespace Estoque.Services.Interfaces
{
    public interface IEstoqueService
    {
        Task<List<EstoqueModel>?> FindAll();
        Task<EstoqueModel?> FindById(int id);
        Task Create(ProdutoCreateViewModel produto);
        Task Update(ProdutoCreateViewModel produto, int id);
        Task Delete(int id);
        Task<List<EstoqueModel>?> Carrinho();
        Task AddCarrinho(int id);
        Task RemoverCarrinho(int id);
    }
}