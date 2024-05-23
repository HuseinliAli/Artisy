using CatalogService.Application.Repositories;
using MediatR;

namespace CatalogService.Application.Features.Genres.Queries.GetAll
{
    public class GetAllGenresQueryHandler(ICatalogGenreRepository genreRepository) : IRequestHandler<GetAllGenresQuery, List<GenreDto>>
    {
        public async Task<List<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var result = genreRepository.GetAll().Select(g => new GenreDto(g.Id, g.Name)).ToList();
            return result;
        }
    }
}
