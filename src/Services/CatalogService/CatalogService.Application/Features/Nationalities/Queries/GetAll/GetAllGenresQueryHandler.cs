using CatalogService.Application.Features.Mediums.Queries.GetAll;
using CatalogService.Application.Repositories;
using MediatR;

namespace CatalogService.Application.Features.Nationalities.Queries.GetAll
{
    public class GetAllNationalitiesQueryHandler(ICatalogArtistNationalityRepository nationalityRepository) : IRequestHandler<GetAllNationalitiesQuery, List<NationalityDto>>
    {
        public async Task<List<NationalityDto>> Handle(GetAllNationalitiesQuery request, CancellationToken cancellationToken)
        {
            var result = nationalityRepository.GetAll().Select(g => new NationalityDto(g.Id, g.Nationality)).ToList();
            return result;
        }
    }
}
