using CatalogService.Application.Repositories;
using CatalogService.Domain.Models.Entities;
using CatalogService.Infrastructure.Contexts;

namespace CatalogService.Infrastructure.Repositories
{
    public class CatalogArtistNationalityRepository : GenericRepository<CatalogArtistNationality, int>, ICatalogArtistNationalityRepository
    {
        public CatalogArtistNationalityRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
