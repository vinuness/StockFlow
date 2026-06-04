using Estoque.Models.Cliente;
using Estoque.Services.Interfaces;

namespace Estoque.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://localhost:7238/api/Cliente";

        public ClienteService(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient();
        }

        public async Task Create(ClienteModel cliente)
        {
            var response = await _http.PostAsJsonAsync($"{BaseUrl}/save", cliente);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception(erro);
            }
        }

        public async Task<LoginResponse?> Login(LoginModel login)
        {
            var response = await _http.PostAsJsonAsync($"{BaseUrl}/login", login);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<ClienteModel?> FindByEmail(string email)
        {
            return await _http.GetFromJsonAsync<ClienteModel>($"{BaseUrl}/findByEmail/{email}");
        }
    }
}