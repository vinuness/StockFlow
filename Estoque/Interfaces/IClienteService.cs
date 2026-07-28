using Estoque.Models.Cliente;

namespace Estoque.Services.Interfaces
{
    public interface IClienteService
    {
        Task Create(ClienteModel cliente);
        Task<LoginResponse?> Login(LoginModel login);
        Task<ClienteModel?> FindByEmail(string email);
        Task<EnderecoModel> AddAdress(string email, EnderecoModel endereco);
        Task RemoveAdress(string email, int id);
        Task SetPrincipalAdress(string email, int id);
        Task<EnderecoModel> FindById(int id, string email);
        Task UpdateAdress(string email, int id, EnderecoModel endereco);
    }
}