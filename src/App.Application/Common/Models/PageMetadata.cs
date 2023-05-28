namespace App.Application.Common.Models;
public sealed class PageMetadata
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPageCount { get; set; }
    public int TotalItemsCount { get; set; }

    public PageMetadata(int page, int pageSize, int totalItemsCount)
    {
        CurrentPage = page;
        PageSize = pageSize;
        TotalItemsCount = totalItemsCount;
        TotalPageCount = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
    }
}
