using CatalogService.Application.Repositories;
using CatalogService.Domain.Models.Entities;
using CatalogService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class CatalogItemCatalogColorPaletteRepository : GenericRepository<CatalogItemCatalogColorPalette, int>, ICatalogItemCatalogColorPaletteRepository
    {
        public CatalogItemCatalogColorPaletteRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
