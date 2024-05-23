using CatalogService.Application.Features.Mediums.Queries.GetAll;
using CatalogService.Application.Repositories;
using MediatR;

namespace CatalogService.Application.Features.Genres.Queries.GetAll
{
    public class GetAllMediumsQueryHandler(ICatalogMediumRepository mediumRepository) : IRequestHandler<GetAllMediumsQuery, List<MediumDto>>
    {
        public async Task<List<MediumDto>> Handle(GetAllMediumsQuery request, CancellationToken cancellationToken)
        {
            var result = mediumRepository.GetAll().Select(g => new MediumDto(g.Id, g.Name)).ToList();
            return result;
        }
    }
}
