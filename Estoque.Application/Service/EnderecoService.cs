using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.IServices;
using Estoque.Domain.Interfaces.Repositories;

namespace Estoque.Domain.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Endereco>> FindAll()
        {
            return await _repository.FindAll();
        }

        public async Task<Endereco> FindById(string email, int id)
        {
            return await _repository.FindById(email, id);
        }

        public async Task SetPrincipalAdress(string email, int id)
        {
            await _repository.SetPrincipalAdress(email, id);
        }

        public async Task Save(string email, EnderecoDTO endereco)
        {
            await _repository.Save(email, endereco);
        }

        public async Task Update(string email, int id, Endereco endereco)
        {
            await _repository.Update(email, id, endereco);
        }

        public async Task Delete(string email, int id)
        {
            await _repository.Delete(email, id);
        }
    }
}