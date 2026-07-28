using Estoque.Domain.Entities.Clientes;

namespace Estoque.Domain.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<List<Endereco>> FindAll();
        Task SetPrincipalAdress(string email, int id);
        Task<Endereco> FindById(string email, int id);
        Task Save(string email, EnderecoDTO endereco);
        Task Update(string email, int id, Endereco endereco);
        Task Delete(string email, int id);
    }
}