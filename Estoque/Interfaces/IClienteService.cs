using Estoque.Models.Cliente;

namespace Estoque.Services.Interfaces
{
    public interface IClienteService
    {
        Task Create(ClienteModel cliente);
        Task<LoginResponse?> Login(LoginModel login);
        Task<ClienteModel?> FindByEmail(string email);
        Task<EnderecoModel> AddEndereco(string email, EnderecoModel endereco);
    }
}