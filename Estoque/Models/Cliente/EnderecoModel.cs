using System.Text.Json.Serialization;

namespace Estoque.Models.Cliente
{
    public class EnderecoModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool Principal { get; set; }

        [JsonIgnore]
        public List<ClienteModel> Clientes { get; set; } = new List<ClienteModel>();

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
    }
}
