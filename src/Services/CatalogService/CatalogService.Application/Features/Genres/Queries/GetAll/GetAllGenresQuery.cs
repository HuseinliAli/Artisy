using MediatR;

namespace CatalogService.Application.Features.Genres.Queries.GetAll
{
    public class GetAllGenresQuery : IRequest<List<GenreDto>>
    {

    }
}
