using Estoque.Domain.Entities;
using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;
using System.Security.Claims;

namespace Estoque.Application.Service
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _repo;
        private readonly ICarrinhoRepository _car;

        public ClienteService(IClienteRepository repo, ICarrinhoRepository car)
        {
            _repo = repo;
            _car = car;
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Cliente>> FindAll()
        {
            List<Cliente> clientes = await _repo.FindAll();
            return clientes;
        }

        public async Task<Cliente> FindById(int id)
        {
            Cliente cliente = await _repo.FindById(id);
            return cliente;
        }

        public async Task<Cliente> FindByEmail(string email)
        {
            return await _repo.FindByEmail(email);
        }

        public async Task<Cliente> Save(Cliente cliente)
        {
            //Transforma a senha em hash usando BCrypt
            cliente.Senha = HashSenha(cliente.Senha);
            await _repo.Save(cliente);
            return cliente;
        }

        public async Task Update(Cliente cliente, int id)
        {
            await _repo.Update(cliente, id);
        }

        public async Task<LoginResponse> Login(Login login)
        {
            var cliente = await _repo.FindByEmail(login.Email);

            if (cliente == null)
            {
                return null;
            }

            if (!VerificarSenha(login.Senha, cliente.Senha))
            {
                return null;
            }


            //retorna o cliente logado com o token JWT
            var response = new LoginResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Token = _repo.GenerateToken(cliente)
            };

            return response;
        }

        //metodo para transformar a senha em hash usando BCrypt
        public string HashSenha(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //metodo para verificar a senha usando BCrypt
        public bool VerificarSenha(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
