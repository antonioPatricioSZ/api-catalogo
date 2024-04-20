using APICatalogo.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Infrastructure;

public static class Extension {

    public static async Task<PagedList<T>> GetPaged<T>(
        this IQueryable<T> query,
        int page,
        int pageSize
    ) where T : class {

        var result = new PagedList<T>();

        result.Page = page;
        result.PageSize = pageSize;
        result.ItemsCount = await query.CountAsync();

        var pageCount = (double)result.ItemsCount / pageSize;
        result.TotalPages = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;

        result.Items = await query.Skip(skip).Take(pageSize).ToListAsync();

        return result;

    } 

}
