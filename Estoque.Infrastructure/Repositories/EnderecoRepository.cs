using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infra.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly AppDbContext _context;

        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Endereco>> FindAll()
        {
            return await _context.Enderecos
                .Include(e => e.Clientes)
                .ToListAsync();
        }

        public async Task<Endereco> FindById(string email, int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Email == email);

            return cliente.Enderecos
                .FirstOrDefault(e => e.Id == id);
        }

        public async Task Save(string email, EnderecoDTO dto)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Email == email);

            if (cliente == null) 
                throw new Exception("Cliente não encontrado");

            var endereco = new Endereco
            {
                Rua = dto.Rua,
                Numero = dto.Numero,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Cep = dto.Cep
            };

            if(cliente.Enderecos.Count() == 0)
            {
                endereco.Principal = true;
            }

            cliente.Enderecos.Add(endereco);

            _context.Enderecos.Add(endereco);

            _context.Clientes.Update(cliente);

            await _context.SaveChangesAsync();

        }

        public async Task SetPrincipalAdress(string email, int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Email == email);

            foreach(var endereco in cliente.Enderecos)
            {
                if(endereco.Id == id)
                {
                    endereco.Principal = true;
                }
                else
                {
                    endereco.Principal = false;
                }
            }

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task Update(string email, int id, Endereco endereco)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Email == email);

            foreach(var cliEndereco in cliente.Enderecos)
            {
                if (cliEndereco.Id.Equals(id))
                {
                    cliEndereco.Id = id;
                    cliEndereco.Numero = endereco.Numero;
                    cliEndereco.Cep = endereco.Cep;
                    cliEndereco.Rua = endereco.Rua;
                    cliEndereco.Bairro = endereco.Bairro;
                    cliEndereco.Cidade = endereco.Cidade;
                    cliEndereco.Estado = endereco.Estado;

                    _context.Enderecos.Update(cliEndereco);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string email, int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Email == email);

            foreach(var endereco in cliente.Enderecos)
            {
                if (endereco.Id.Equals(id))
                {
                    _context.Enderecos.Remove(endereco);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}