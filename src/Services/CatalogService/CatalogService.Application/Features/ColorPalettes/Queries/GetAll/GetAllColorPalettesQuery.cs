using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Features.ColorPalettes.Queries.GetAll
{
    public class GetAllColorPalettesQuery : IRequest<List<ColorPaletteDto>>
    {
    }
}
