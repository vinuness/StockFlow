using Estoque.API.DTO;
using Estoque.Application.Service;
using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Produtos;
using Estoque.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Estoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _service;
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public ProdutoController(IProdutoService service, IConfiguration config)
        {
            _service = service;
            _key = Encoding.UTF8.GetBytes(config["ImagemByte:ImgKey"]);
            _iv = Encoding.UTF8.GetBytes(config["ImagemByte:ImgIv"]);
        }

        [HttpGet("findAll")]
        [Authorize(Roles = "Admin,Operador,Cliente")]
        public async Task<ActionResult<List<Produto>>> FindAll()
        {
            List<Produto> produtos = await _service.FindAll();
            return Ok(produtos);
        }

        [HttpGet("findById/{id}")]
        [Authorize(Roles = "Admin,Operador,Cliente")]
        public async Task<ActionResult<Produto>> FindById(int id)
        {
            Produto produto = await _service.FindById(id);
            if (produto == null)
            {
                return NotFound("Produto não existente");
            }
            return Ok(produto);
        }

        [HttpPost("save")]
        [Authorize(Roles = "Admin,Operador")]
        public async Task<ActionResult<Produto>> Save([FromForm] ProdutoSaveDTO produto)
        {
            var produtoModel = new Produto
            {
                Nome = produto.Nome,
                Quantidade = produto.Quantidade,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Tamanho = produto.Tamanho,
                Cor = produto.Cor,
                CategoriaId = produto.CategoriaId,
                FornecedorId = produto.FornecedorId
            };

            List<ImagemModel> list_imagem = new();

            // Cria o diretório "uploaded_files_encrypted" se não existir
            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploaded_files_encrypted"); 

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var imagem in produto.Imagem)
            {
                // Define o caminho completo do arquivo criptografado
                var encryptedFilePath = Path.Combine(path, imagem.FileName + ".enc");

                using (var aes = Aes.Create())
                {
                    aes.Key = _key;
                    aes.IV = _iv;

                    // Cria um fluxo de arquivo para escrever o arquivo criptografado
                    using (var cryptorTransform = aes.CreateEncryptor())
                    using (var fileStream = new FileStream(encryptedFilePath, FileMode.Create))
                    using (var cryptoStream = new CryptoStream(fileStream, cryptorTransform, CryptoStreamMode.Write))
                    {
                        await imagem.CopyToAsync(cryptoStream);

                    }
                }

                var img = new ImagemModel
                {
                    FileName = imagem.FileName,
                    ContentType = imagem.ContentType,
                    Path = encryptedFilePath
                };
                list_imagem.Add(img);
            }

            produtoModel.Imagens = list_imagem;

            await _service.Save(produtoModel);
            return Ok("produto salvo com sucesso");
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin,Operador")]
        public async Task<ActionResult> Update(int id, [FromForm] ProdutoSaveDTO dto)
        {
            List<ImagemModel> list_img = new();

            var produto = new Produto
            {
                Id = id,
                Nome = dto.Nome,
                Quantidade = dto.Quantidade,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                Cor = dto.Cor,
                Tamanho = dto.Tamanho,
                CategoriaId = dto.CategoriaId,
                FornecedorId = dto.FornecedorId
            };

            string encryptedFilePath = null;
            string fileName = null;
            string contentType = null;

            if(dto.Imagem != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "uploaded_files_encrypted");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var imagem in dto.Imagem)
                {
                    fileName = imagem.FileName + ".enc";
                    encryptedFilePath = Path.Combine(path, fileName);

                    using var aes = Aes.Create();
                    aes.Key = _key;
                    aes.IV = _iv;

                    using var cryptorTransform = aes.CreateEncryptor();
                    using var fileStream = new FileStream(encryptedFilePath, FileMode.Create);
                    using var cryptoStream = new CryptoStream(fileStream, cryptorTransform, CryptoStreamMode.Write);

                    await imagem.CopyToAsync(cryptoStream);

                    contentType = imagem.ContentType;

                    var img = new ImagemModel
                    {
                        FileName = fileName,
                        ContentType = contentType,
                        Path = encryptedFilePath
                    };

                    list_img.Add(img);
                }

            }

            if (list_img.Any())
            {
                produto.Imagens = list_img;
            }

            await _service.Update(produto);
            return Ok("Produto atualizado com sucesso");
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin,Operador")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Produto deletado com sucesso");
        }

        [HttpGet("carrinho/{email}")]
        public async Task<ActionResult<List<ItemCarrinho>>> ListarProdutosCarrinho(string email)
        {
            List<ItemCarrinho> produtos = await _service.ListarProdutosCarrinho(email);
            return Ok(produtos);
        }

        [HttpPut("carrinho/{email}/add/{id}")]
        [Authorize(Roles = "Admin,Operador,Cliente")]
        public async Task<ActionResult> AddCarrinho(string email, int id)
        {
            await _service.AddCarrinho(email, id);
            return Ok("Produto adicionado ao carrinho com sucesso");
        }

        [HttpPut("carrinho/{email}/remove/{id}")]
        [Authorize(Roles = "Admin,Operador,Cliente")]
        public async Task<ActionResult> RemoverCarrinho(string email, int id)
        {
            await _service.RemoverCarrinho(email, id);
            return Ok("Produto removido do carrinho com sucesso");
        }

        [HttpPut("carrinho/clean/{email}")]
        [Authorize(Roles = "Admin,Operador,Cliente")]
        public async Task<ActionResult> limparCarrinho(string email)
        {
            await _service.limparCarrinho(email);
            return Ok("Produto removido do carrinho com sucesso");
        }

        [HttpGet("mais_vendidos")]
        public async Task<ActionResult<List<ProdutoMaisVendidoDTO>>> MaisVendidos()
        {
            var produtos = await _service.ProdutosMaisVendidos();
            return Ok(produtos);
        }

        [HttpGet("imagem/{imagemId}")]
        public async Task<IActionResult> ObterImagem(int imagemId)
        {
            var imagem = await _service.buscarImagem(imagemId);

            if (imagem == null)
                return NotFound();

            if (!System.IO.File.Exists(imagem.Path))
                return NotFound($"Arquivo de imagem não encontrado em {imagem.Path}.");

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var fileStream = new FileStream(imagem.Path, FileMode.Open);
            using var decryptor = aes.CreateDecryptor();
            using var cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read);
            using var memory = new MemoryStream();

            await cryptoStream.CopyToAsync(memory);

            return File(memory.ToArray(), imagem.ContentType);
        }
    }
}
