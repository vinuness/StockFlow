using Estoque.Models;
using Estoque.Services.Interfaces;

namespace Estoque.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _http;

        public CategoriaService(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient("API");
        }

        public async Task<List<CategoriaModel>> FindAll()
        {
            return await _http.GetFromJsonAsync<List<CategoriaModel>>("https://localhost:7238/api/Categoria/findAll") ?? new List<CategoriaModel>();
        }

        public async Task<CategoriaModel?> FindById(int id)
        {
            return await _http.GetFromJsonAsync<CategoriaModel>($"https://localhost:7238/api/Categoria/findById/{id}");
        }

        public async Task Create(CategoriaModel categoria)
        {
            await _http.PostAsJsonAsync("https://localhost:7238/api/Categoria/save", categoria);
        }

        public async Task Update(CategoriaModel categoria)
        {
            await _http.PutAsJsonAsync($"https://localhost:7238/api/Categoria/update/{categoria.Id}", categoria);
        }

        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"https://localhost:7238/api/Categoria/delete/{id}");
        }
    }
}