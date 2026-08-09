using Estoque.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Infrastructure.Helper
{
    public static class PaginationHelper
    {
        public static async Task<PagedList<T>> CreateAsync<T>
            (IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            var count = await source.CountAsync();
            var items =  await source
                .Skip((pageNumber - 1) * pageSize) //pulos, sendo por exemplo ((2-1)*10) pega a partir do 10 item, ou seja, a segunda pagina
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, pageNumber, pageSize, count); 
        }
    }
}
