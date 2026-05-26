using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Application.Service
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _repo;
        public FornecedorService(IFornecedorRepository repo)
        {
            _repo = repo;
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Fornecedor>> FindAll()
        {
            List<Fornecedor> fornecedores = await _repo.FindAll();
            return fornecedores;
        }

        public async Task<Fornecedor> FindById(int id)
        {
            Fornecedor fornecedor = await _repo.FindById(id);
            return fornecedor;
        }

        public async Task<Fornecedor> Save(Fornecedor fornecedor)
        {
            await _repo.Save(fornecedor);
            return fornecedor;
        }

        public async Task Update(Fornecedor fornecedor, int id)
        {
            await _repo.Update(fornecedor, id);
        }
    }
}
