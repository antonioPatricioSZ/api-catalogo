using APICatalogo.Domain.Entities;

namespace APICatalogo.Domain.Pagination;

public class PagedList<T> : List<T> where T : class
{

    public PagedList() { }

    public PagedList(int page, int totalPages, int pageSize, int itemsCount, List<T> items)
    {
        Page = page;
        TotalPages = totalPages;
        PageSize = pageSize;
        ItemsCount = itemsCount;
        Items = items;
    }

    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int ItemsCount { get; set; }
    public List<T> Items { get; set; }
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;

}
