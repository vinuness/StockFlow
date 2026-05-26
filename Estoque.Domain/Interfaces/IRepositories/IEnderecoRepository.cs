using Estoque.Domain.Entities.Clientes;

namespace Estoque.Domain.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<List<Endereco>> FindAll();
        Task<Endereco> FindById(int id);
        Task Save(Endereco endereco);
        Task Update(Endereco endereco);
        Task Delete(int id);
    }
}