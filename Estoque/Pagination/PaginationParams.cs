using System.ComponentModel.DataAnnotations;

namespace Estoque.Pagination
{
    public class PaginationParams
    {
        [Range(1, int.MaxValue)]
        public int pageNumber { get; set; } = 1;

        [Range(1, 50, ErrorMessage = "O maximo de itens por pagina é 50")]
        public int pageSize { get; set; } = 4;
    }
}
