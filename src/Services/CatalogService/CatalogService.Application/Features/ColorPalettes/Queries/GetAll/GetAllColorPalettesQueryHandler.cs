using CatalogService.Application.Repositories;
using MediatR;

namespace CatalogService.Application.Features.ColorPalettes.Queries.GetAll
{
    public class GetAllColorPalettesQueryHandler(ICatalogColorPaletteRepository colorPaletteRepository) : IRequestHandler<GetAllColorPalettesQuery, List<ColorPaletteDto>>
    {
        public async Task<List<ColorPaletteDto>> Handle(GetAllColorPalettesQuery request, CancellationToken cancellationToken)
        {
            var result = colorPaletteRepository.GetAll().Select(cp => new ColorPaletteDto(cp.Id, cp.Name, cp.HexCode));
            return result.ToList();
        }
    }
}
