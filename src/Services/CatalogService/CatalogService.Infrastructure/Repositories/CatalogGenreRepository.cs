using CatalogService.Application.Repositories;
using CatalogService.Domain.Models.Entities;
using CatalogService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class CatalogGenreRepository : GenericRepository<CatalogGenre, int>, ICatalogGenreRepository
    {
        public CatalogGenreRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
