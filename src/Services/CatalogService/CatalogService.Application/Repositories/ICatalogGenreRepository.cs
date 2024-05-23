using CatalogService.Domain.Models.Entities;

namespace CatalogService.Application.Repositories
{
    public interface ICatalogGenreRepository : IGenericRepository<CatalogGenre, int>
    {
    }
}
