using MediatR;

namespace CatalogService.Application.Features.Mediums.Queries.GetAll
{
    public class GetAllMediumsQuery : IRequest<List<MediumDto>>
    {

    }
}
