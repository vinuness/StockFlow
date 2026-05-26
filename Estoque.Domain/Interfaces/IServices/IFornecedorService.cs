using Estoque.Domain.Entities.Produtos;

namespace Estoque.Domain.Interfaces.IServices
{
    public interface IFornecedorService
    {
        public Task<List<Fornecedor>> FindAll();
        public Task<Fornecedor> FindById(int id);
        public Task<Fornecedor> Save(Fornecedor fornecedor);
        public Task Update(Fornecedor fornecedor, int id);
        public Task Delete(int id);
    }
}
