using Estoque.Domain.Entities.Clientes;
using System.Text.Json.Serialization;

namespace Estoque.Domain.Entities.Clientes
{
    public class Endereco
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool Principal { get; set; } = false;

        [JsonIgnore]
        public List<Cliente> Clientes { get; set; } = new();
    }

    public class EnderecoDTO
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        [JsonIgnore]
        public List<Cliente> Clientes { get; set; } = new();
    }
}