using Architecture.Application.Core.Structure.Models;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infra.Data.Structure.Extensions;

public static class PaginationExtension
{
    public static async Task<PagedResult<T>> PaginationAsync<T>(this IQueryable<T> elements, int pageNumber, int pageSize)
    {
        var count = await elements.CountAsync();

        var itens = await elements
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>(itens, pageNumber, pageSize, count);
    }
}
