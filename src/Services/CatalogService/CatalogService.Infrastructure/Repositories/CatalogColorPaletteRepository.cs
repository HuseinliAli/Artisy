using CatalogService.Application.Repositories;
using CatalogService.Domain.Models.Entities;
using CatalogService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class CatalogColorPaletteRepository : GenericRepository<CatalogColorPalette, int>, ICatalogColorPaletteRepository
    {
        public CatalogColorPaletteRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
