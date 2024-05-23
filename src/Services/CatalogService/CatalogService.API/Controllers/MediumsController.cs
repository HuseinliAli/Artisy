using CatalogService.Application.Features.Mediums.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediumsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllMediumsQuery request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
