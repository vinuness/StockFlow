using Estoque.Models.Cliente;
using Estoque.Services.Interfaces;
using System.Net.Http.Headers;

namespace Estoque.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://localhost:7238/api/Cliente";
        private const string Usuario = "https://localhost:7238/api/Usuario";

        public ClienteService(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient("API");
        }

        public async Task Create(ClienteModel cliente)
        {
            var response = await _http.PostAsJsonAsync($"{Usuario}/cadastro", cliente);

            Console.WriteLine(response);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception(erro);
            }
        }

        public async Task<LoginResponse?> Login(LoginModel login)
        {
            var response = await _http.PostAsJsonAsync($"{Usuario}/login", login);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<LoginResponse>()    ;
        }

        public async Task<ClienteModel?> FindByEmail(string email)
        {
            var cliente = await _http.GetFromJsonAsync<ClienteModel>($"{BaseUrl}/findByEmail/{email}");

            if(cliente == null)
            {
                return null;
            }

            return cliente;
        }   

        public async Task<EnderecoModel> AddAdress(string email, EnderecoModel endereco)
        {
            var response = await _http.PostAsJsonAsync($"https://localhost:7238/api/Endereco/save/user/{email}", endereco);
            return await response.Content.ReadFromJsonAsync<EnderecoModel>();
        }

        public async Task SetPrincipalAdress(string email, int id)
        {
            await _http.PutAsync($"https://localhost:7238/api/Endereco/set/{email}/principal/adress/{id}", null);
        }

        public async Task RemoveAdress(string email, int id)
        {
            await _http.DeleteAsync($"https://localhost:7238/api/Endereco/delete/{email}/adress/{id}");
        }

        public async Task UpdateAdress(string email, int id, EnderecoModel endereco)
        {
            await _http.PutAsJsonAsync($"https://localhost:7238/api/Endereco/update/{email}/adress/{id}", endereco);
        }

        public async Task<EnderecoModel> FindById(int id, string email)
        {
            return await _http.GetFromJsonAsync<EnderecoModel>($"https://localhost:7238/api/Endereco/find/{email}/adress/{id}");
        }
    }
}