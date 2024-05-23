using CatalogService.Application.Features.Nationalities.Queries.GetAll;
using MediatR;

namespace CatalogService.Application.Features.Nationalities.Queries.GetAll
{
    public class GetAllNationalitiesQuery : IRequest<List<NationalityDto>>
    {
    }
}
