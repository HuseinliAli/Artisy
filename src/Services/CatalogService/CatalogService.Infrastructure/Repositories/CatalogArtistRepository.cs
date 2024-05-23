using CatalogService.Application.Repositories;
using CatalogService.Domain.Models.Entities;
using CatalogService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class CatalogArtistRepository : GenericRepository<CatalogArtist, int>, ICatalogArtistRepository
    {
        public CatalogArtistRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
