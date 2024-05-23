namespace CatalogService.Infrastructure.ViewModels
{
    public class PaginatedItems<TEntity> where TEntity :class, new()
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

        public PaginatedItems(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
