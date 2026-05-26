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
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repo;
        public CategoriaService(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Categoria>> FindAll()
        {
            List<Categoria> categorias = await _repo.FindAll();
            return categorias;
        }

        public async Task<Categoria> FindById(int id)
        {
            Categoria categoria = await _repo.FindById(id);
            return categoria;
        }

        public async Task<Categoria> Save(Categoria categoria)
        {
            await _repo.Save(categoria);
            return categoria;
        }

        public async Task Update(Categoria categoria, int id)
        {
            await _repo.Update(categoria, id);
        }
    }
}
