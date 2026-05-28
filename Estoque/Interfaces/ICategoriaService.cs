using Estoque.Models;

namespace Estoque.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaModel>> FindAll();
        Task<CategoriaModel?> FindById(int id);
        Task Create(CategoriaModel categoria);
        Task Update(CategoriaModel categoria);
        Task Delete(int id);
    }
}