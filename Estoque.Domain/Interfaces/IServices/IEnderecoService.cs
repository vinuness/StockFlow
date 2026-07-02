using Estoque.Domain.Entities.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Domain.Interfaces.IServices
{
    public interface IEnderecoService
    {
        Task<List<Endereco>> FindAll();
        Task<Endereco> FindById(int id);
        Task Save(string email, EnderecoDTO endereco);
        Task Update(Endereco endereco);
        Task Delete(int id);
    }
}
