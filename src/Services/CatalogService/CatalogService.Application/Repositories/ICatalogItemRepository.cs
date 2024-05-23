using CatalogService.Domain.Models.Entities;

namespace CatalogService.Application.Repositories
{
    public interface ICatalogItemRepository : IGenericRepository<CatalogItem, int>
    {
    }
}
