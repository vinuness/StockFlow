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
        Task<Endereco> FindById(string email, int id);
        Task SetPrincipalAdress(string email, int id);
        Task Save(string email, EnderecoDTO endereco);
        Task Update(string email, int id, Endereco endereco);
        Task Delete(string email, int id);
    }
}
