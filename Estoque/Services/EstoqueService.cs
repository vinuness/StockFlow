using Estoque.Models;
using Estoque.Services.Interfaces;
using System.Globalization;
using System.Net.Http.Headers;

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
            _http = factory.CreateClient("API");
        }

        public async Task<List<EstoqueModel>?> FindAll()
        {
            return await _http.GetFromJsonAsync<List<EstoqueModel>>($"{ProdutoUrl}/findAll");
        }

        public async Task<EstoqueModel?> FindById(int id)
        {
            var produto = await _http.GetFromJsonAsync<EstoqueModel>($"{ProdutoUrl}/findById/{id}");

            return produto;
        }

        public async Task Create(ProdutoCreateViewModel produto)
        {
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent(produto.Nome), "Nome");
            form.Add(new StringContent(produto.Quantidade.ToString()), "Quantidade");
            form.Add(new StringContent(produto.Descricao ?? ""), "Descricao");
            form.Add(new StringContent(produto.Preco.ToString(CultureInfo.InvariantCulture)), "Preco");
            form.Add(new StringContent(produto.Tamanho ?? ""), "Tamanho");
            form.Add(new StringContent(produto.Cor ?? ""), "Cor");
            form.Add(new StringContent(produto.CategoriaId.ToString()), "CategoriaId");
            form.Add(new StringContent(produto.FornecedorId.ToString()), "FornecedorId");

            if (produto.Imagem != null)
            {
                var streamContent = new StreamContent(produto.Imagem.OpenReadStream());

                streamContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(produto.Imagem.ContentType);

                form.Add(streamContent, "Imagem", produto.Imagem.FileName);
            }

            var response = await _http.PostAsync($"{ProdutoUrl}/save", form);

            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Status: {response.StatusCode}\n\n{body}");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task Update(ProdutoCreateViewModel produto, int id)
        {
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent(produto.Nome), "Nome");
            form.Add(new StringContent(produto.Quantidade.ToString()), "Quantidade");
            form.Add(new StringContent(produto.Descricao ?? ""), "Descricao");
            form.Add(new StringContent(produto.Preco.ToString(CultureInfo.InvariantCulture)), "Preco");
            form.Add(new StringContent(produto.Tamanho ?? ""), "Tamanho");
            form.Add(new StringContent(produto.Cor ?? ""), "Cor");
            form.Add(new StringContent(produto.CategoriaId.ToString()), "CategoriaId");
            form.Add(new StringContent(produto.FornecedorId.ToString()), "FornecedorId");

            if (produto.Imagem != null)
            {
                var streamContent = new StreamContent(produto.Imagem.OpenReadStream());

                streamContent.Headers.ContentType =
                    new MediaTypeHeaderValue(produto.Imagem.ContentType);

                form.Add(streamContent, "Imagem", produto.Imagem.FileName);
            }

            var request = new HttpRequestMessage(HttpMethod.Put, $"{ProdutoUrl}/update/{id}")
            {
                Content = form
            };

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
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