using System.Net.Http.Json;
using Estoque.Models;
using Estoque.Services.Interfaces;

namespace Estoque.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://localhost:7238/api/Fornecedor";

        public FornecedorService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API");
        }

        public async Task<List<FornecedorModel>?> FindAll()
        {
            return await _http.GetFromJsonAsync<List<FornecedorModel>>($"{BaseUrl}/findAll");
        }

        public async Task<FornecedorModel?> FindById(int id)
        {
            return await _http.GetFromJsonAsync<FornecedorModel>($"{BaseUrl}/findById/{id}");
        }

        public async Task Create(FornecedorModel fornecedor)
        {
            await _http.PostAsJsonAsync($"{BaseUrl}/save", fornecedor);
        }

        public async Task Update(FornecedorModel fornecedor)
        {
            await _http.PutAsJsonAsync($"{BaseUrl}/update/{fornecedor.Id}", fornecedor);
        }

        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"{BaseUrl}/delete/{id}");
        }
    }
}