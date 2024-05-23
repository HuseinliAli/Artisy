using CatalogService.Application.Features.ColorPalettes.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorPalettesController(IMediator mediator): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute]GetAllColorPalettesQuery request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
