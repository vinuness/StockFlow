using Estoque.Models;

namespace Estoque.Services.Interfaces
{
    public interface IFornecedorService
    {
        Task<List<FornecedorModel>?> FindAll();
        Task<FornecedorModel?> FindById(int id);
        Task Create(FornecedorModel fornecedor);
        Task Update(FornecedorModel fornecedor);
        Task Delete(int id);
    }
}