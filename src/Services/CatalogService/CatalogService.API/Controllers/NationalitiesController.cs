using CatalogService.Application.Features.Nationalities.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalitiesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllNationalitiesQuery request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
