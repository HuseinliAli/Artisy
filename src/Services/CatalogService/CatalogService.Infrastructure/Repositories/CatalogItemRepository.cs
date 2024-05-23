using CatalogService.Application.Repositories;
using CatalogService.Domain.Models.Entities;
using CatalogService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class CatalogItemRepository : GenericRepository<CatalogItem, int>, ICatalogItemRepository
    {
        public CatalogItemRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
