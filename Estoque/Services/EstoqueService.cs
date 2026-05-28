using Estoque.Models;
using Estoque.Services.Interfaces;

namespace Estoque.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly HttpClient _http;
        private const string ProdutoUrl = "https://localhost:7238/api/Produto";
        private const string CategoriaUrl = "https://localhost:7238/api/Categoria";
        private const string FornecedorUrl = "https://localhost:7238/api/Fornecedor";

        public EstoqueService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<List<EstoqueModel>?> FindAll()
        {
            return await _http.GetFromJsonAsync<List<EstoqueModel>>($"{ProdutoUrl}/findAll");
        }

        public async Task<EstoqueModel?> FindById(int id)
        {
            var produto = await _http.GetFromJsonAsync<EstoqueModel>($"{ProdutoUrl}/findById/{id}");

            produto.Categoria = await _http.GetFromJsonAsync<CategoriaModel>($"{CategoriaUrl}/findById/{produto.CategoriaId}");

            produto.Fornecedor = await _http.GetFromJsonAsync<FornecedorModel>($"{FornecedorUrl}/findById/{produto.FornecedorId}");

            return produto;
        }

        public async Task Create(EstoqueModel produto)
        {
            produto.Categoria = await _http.GetFromJsonAsync<CategoriaModel>($"{CategoriaUrl}/findById/{produto.CategoriaId}");

            produto.Fornecedor = await _http.GetFromJsonAsync<FornecedorModel>($"{FornecedorUrl}/findById/{produto.FornecedorId}");

            await _http.PostAsJsonAsync($"{ProdutoUrl}/save", produto);
        }

        public async Task Update(EstoqueModel produto)
        {
            produto.Categoria = await _http.GetFromJsonAsync<CategoriaModel>($"{CategoriaUrl}/findById/{produto.CategoriaId}");

            produto.Fornecedor = await _http.GetFromJsonAsync<FornecedorModel>($"{FornecedorUrl}/findById/{produto.FornecedorId}");

            await _http.PutAsJsonAsync($"{ProdutoUrl}/update/{produto.Id}", produto);
        }

        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"{ProdutoUrl}/delete/{id}");
        }

        public async Task<List<EstoqueModel>?> Carrinho()
        {
            return await _http.GetFromJsonAsync<List<EstoqueModel>>($"{ProdutoUrl}/carrinho");
        }

        public async Task AddCarrinho(int id)
        {
            var produto = await _http.GetFromJsonAsync<EstoqueModel>($"{ProdutoUrl}/findById/{id}");

            if (produto.Status == StatusProduto.CATALOGADO)
            {
                produto.Status = StatusProduto.CARRINHO;

                await _http.PutAsJsonAsync($"{ProdutoUrl}/carrinho/add/{id}", produto);
            }
        }

        public async Task RemoverCarrinho(int id)
        {
            var produto = await _http.GetFromJsonAsync<EstoqueModel>($"{ProdutoUrl}/findById/{id}");

            if (produto.Status == StatusProduto.CARRINHO)
            {
                produto.Status = StatusProduto.CATALOGADO;

                await _http.PutAsJsonAsync($"{ProdutoUrl}/carrinho/remove/{id}", produto);
            }
        }
    }
}