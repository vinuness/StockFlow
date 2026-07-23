using Estoque.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Domain.Entities.Clientes
{
    public class ItemCarrinho
    {
        public int Id { get; set; }

        public int CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
    }
}
