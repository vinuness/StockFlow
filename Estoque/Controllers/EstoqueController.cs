using Estoque.Models;
using Estoque.Pagination;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace Estoque.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IEstoqueService _estoqueService;
        private readonly ICategoriaService _cat;
        private readonly IFornecedorService _for;

        public EstoqueController(IEstoqueService estoqueService, ICategoriaService cat, IFornecedorService forn)
        {
            _estoqueService = estoqueService;
            _cat = cat;
            _for = forn;
        }

        public async Task<IActionResult> Index(PaginationParams paginationParams, int? catId, int? forId)
        {
            var produtos = await _estoqueService.FindAll(paginationParams);

            if (catId.HasValue)
            {
                produtos = produtos
                    .Where(p => p.CategoriaId == catId.Value)
                    .ToList();
            }

            if (forId.HasValue)
            {
                produtos = produtos
                    .Where(p => p.FornecedorId == forId.Value)
                    .ToList();
            }

            var categorias = await _cat.FindAll();
            var fornecedores = await _for.FindAll();

            ViewBag.Categorias = new SelectList(categorias,"Id","Nome",catId);
            ViewBag.Fornecedores = new SelectList(fornecedores,"Id","Nome",forId);
            ViewBag.PageNumber = paginationParams.pageNumber;
            ViewBag.PageSize = paginationParams.pageSize;

            return View(produtos);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categorias = await _cat.FindAll();
            var fornecedores = await _for.FindAll();

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");
            ViewBag.Fornecedores = new SelectList(fornecedores, "Id", "Nome");

            return View(new ProdutoCreateViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var produto = await _estoqueService.FindById(id);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProdutoCreateViewModel produto)
        {
            await _estoqueService.Create(produto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _estoqueService.FindById(id);

            var vm = new ProdutoCreateViewModel
            {
                Nome = produto.Nome,
                Quantidade = produto.Quantidade,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Cor = produto.Cor,
                Tamanho = produto.Tamanho,
                CategoriaId = produto.CategoriaId,
                FornecedorId = produto.FornecedorId
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoCreateViewModel produto, int id)
        {
            await _estoqueService.Update(produto, id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _estoqueService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Carrinho()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if(User.Identity.IsAuthenticated && User.Identity != null)
            {
                var carrinho = await _estoqueService.Carrinho(email);
                return View(carrinho);
            }
            else
            {
                return RedirectToAction("Logar", "Cliente");
            }
        }

        public async Task<IActionResult> AddCarrinho(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _estoqueService.AddCarrinho(email, id);

            return RedirectToAction("Carrinho");
        }

        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _estoqueService.RemoverCarrinho(email, id);

            return RedirectToAction("Carrinho");
        }

        public async Task<IActionResult> LimparCarrinho()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _estoqueService.limparCarrinho(email);

            return RedirectToAction("Carrinho");
        }
    }
}