using Estoque.Models;
using Estoque.Models.Carrinho;
using Estoque.Pagination;
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

        public async Task<List<EstoqueModel>?> FindAll(PaginationParams Pageparams)
        {
            var produtos = await _http.GetFromJsonAsync<List<EstoqueModel>>($"{ProdutoUrl}/findAll?pageNumber={Pageparams.pageNumber}&pageSize={Pageparams.pageSize}");

            return produtos;
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
            form.Add(new StringContent(produto.Preco.ToString(new CultureInfo("pt-BR"))), "Preco");
            form.Add(new StringContent(produto.Tamanho ?? ""), "Tamanho");
            form.Add(new StringContent(produto.Cor ?? ""), "Cor");
            form.Add(new StringContent(produto.CategoriaId.ToString()), "CategoriaId");
            form.Add(new StringContent(produto.FornecedorId.ToString()), "FornecedorId");

            if (produto.Imagem != null && produto.Imagem.Any())
            {
                foreach (var imagem in produto.Imagem)
                {
                    var streamContent = new StreamContent(imagem.OpenReadStream());

                    streamContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue(imagem.ContentType);

                    form.Add(streamContent, "Imagem", imagem.FileName);
                }
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
            form.Add(new StringContent(produto.Preco.ToString(new CultureInfo("pt-BR"))), "Preco");
            form.Add(new StringContent(produto.Tamanho ?? ""), "Tamanho");
            form.Add(new StringContent(produto.Cor ?? ""), "Cor");
            form.Add(new StringContent(produto.CategoriaId.ToString()), "CategoriaId");
            form.Add(new StringContent(produto.FornecedorId.ToString()), "FornecedorId");

            if (produto.Imagem != null && produto.Imagem.Any())
            {
                foreach (var imagem in produto.Imagem)
                {
                    var streamContent = new StreamContent(imagem.OpenReadStream());

                    streamContent.Headers.ContentType =
                        new MediaTypeHeaderValue(imagem.ContentType);

                    form.Add(streamContent, "Imagem", imagem.FileName);
                }
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

        public async Task<List<ItemCarrinho>?> Carrinho(string email)
        {
            return await _http.GetFromJsonAsync<List<ItemCarrinho>>($"{ProdutoUrl}/carrinho/{email}");
        }

        public async Task AddCarrinho(string email, int id)
        {
            await _http.PutAsync($"{ProdutoUrl}/carrinho/{email}/add/{id}", null);
        }

        public async Task RemoverCarrinho(string email, int id)
        {
            await _http.PutAsync($"{ProdutoUrl}/carrinho/{email}/remove/{id}", null);
        }

        public async Task limparCarrinho(string email)
        {
            await _http.PutAsync($"{ProdutoUrl}/carrinho/clean/{email}", null);
        }
    }
}